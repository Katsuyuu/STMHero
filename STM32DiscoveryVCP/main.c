
#define HSE_VALUE ((uint32_t)8000000) /* STM32 discovery uses a 8Mhz external crystal */

#include "stm32f4xx_conf.h"
#include "stm32f4xx.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_exti.h"
#include "usbd_cdc_core.h"
#include "usbd_usr.h"
#include "usbd_desc.h"
#include "usbd_cdc_vcp.h"
#include "usb_dcd_int.h"
#include "stm32f4xx_tim.h"
#include "misc.h"

volatile uint32_t ticker, downTicker;

/*
 * The USB data must be 4 byte aligned if DMA is enabled. This macro handles
 * the alignment, if necessary (it's actually magic, but don't tell anyone).
 */
__ALIGN_BEGIN USB_OTG_CORE_HANDLE  USB_OTG_dev __ALIGN_END;


void init();
void ColorfulRingOfDeath(void);

/*
 * Define prototypes for interrupt handlers here. The conditional "extern"
 * ensures the weak declarations from startup_stm32f4xx.c are overridden.
 */
#ifdef __cplusplus
 extern "C" {
#endif

void SysTick_Handler(void);
void NMI_Handler(void);
void HardFault_Handler(void);
void MemManage_Handler(void);
void BusFault_Handler(void);
void UsageFault_Handler(void);
void SVC_Handler(void);
void DebugMon_Handler(void);
void PendSV_Handler(void);
void OTG_FS_IRQHandler(void);
void OTG_FS_WKUP_IRQHandler(void);

#ifdef __cplusplus
}
#endif

typedef int bool;
#define true 1
#define false 0

uint8_t theByte;
uint8_t A, S, D, F;

bool connected = false;
bool recieved = false;

void GPIOD_Init()
{
	GPIO_InitTypeDef GPIO_InitStructure;

	/* GPIOD Periph clock enable */
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

	/* Configure PD12, PD13, PD14 and PD15 in output pushpull mode */
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2 | GPIO_Pin_3 | GPIO_Pin_6;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_Init(GPIOD, &GPIO_InitStructure);
}

void TIM3_IRQHandler(void)
{
         	if(TIM_GetITStatus(TIM3, TIM_IT_Update) != RESET)
         	{
         		A = !GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_0) * 0xFF;
         		S = !GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_1) * 0xFF;
         		D = !GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_2) * 0xFF;
         		F = !GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_3) * 0xFF;

         		if(VCP_get_char(&theByte))
         		{
         			if(theByte == 'H')
         			{
         				if(recieved)
         				{
         					VCP_put_char('I');
         					connected = true;
         					recieved = false;
         				}else
         				{
         					VCP_put_char('I');
         					recieved = true;
         				}

         			}else if (theByte == 'B')
         			{
						if(recieved)
						{
							VCP_put_char('Y');
		         			connected = false;
		         			recieved = false;
						}else
						{
							VCP_put_char('Y');
         					recieved = true;

						}
					}
         		}

         		if(connected)
				{
					VCP_put_char(0x38);
					VCP_put_char(A);
					VCP_put_char(S);
					VCP_put_char(D);
					VCP_put_char(F);
				}
                // wyzerowanie flagi wyzwolonego przerwania
                TIM_ClearITPendingBit(TIM3, TIM_IT_Update);
         	}
}


int main(void)
{
	/* Set up the system clocks */
	SystemInit();
	SystemCoreClockUpdate();

	GPIOD_Init();

	// USTAWIANIE ZEGARA
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);

	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_TimeBaseStructure.TIM_Period = 2000;
	TIM_TimeBaseStructure.TIM_Prescaler = 1400;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode =  TIM_CounterMode_Up ;
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);

	NVIC_InitTypeDef NVIC_InitStructure;
	// numer przerwania
	NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;
	// priorytet g³ówny
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x00;
	// subpriorytet
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x00;
	// uruchom dany kana³
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	// zapisz wype³nion¹ strukturê do rejestrów
	NVIC_Init(&NVIC_InitStructure);

	// wyczyszczenie przerwania od timera 3 (wyst¹pi³o przy konfiguracji timera)
	TIM_ClearITPendingBit(TIM3, TIM_IT_Update);
	// zezwolenie na przerwania od przepe³nienia dla timera 3
	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);

	TIM_Cmd(TIM3, ENABLE);

	GPIO_InitTypeDef  GPIO_InitStructure;
	// USTAWIANIA PRZYCISKU
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	/* Initialize USB, IO, SysTick, and all those other things you do in the morning */
	init();


	while (1)
	{

	}

	return 0;
}


void init()
{
	/* STM32F4 discovery LEDs */
	GPIO_InitTypeDef LED_Config;

	/* Always remember to turn on the peripheral clock...  If not, you may be up till 3am debugging... */
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);
	LED_Config.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13| GPIO_Pin_14| GPIO_Pin_15;
	LED_Config.GPIO_Mode = GPIO_Mode_OUT;
	LED_Config.GPIO_OType = GPIO_OType_PP;
	LED_Config.GPIO_Speed = GPIO_Speed_25MHz;
	LED_Config.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &LED_Config);



	/* Setup SysTick or CROD! */
	if (SysTick_Config(SystemCoreClock / 1000))
	{
		ColorfulRingOfDeath();
	}


	/* Setup USB */
	USBD_Init(&USB_OTG_dev,
	            USB_OTG_FS_CORE_ID,
	            &USR_desc,
	            &USBD_CDC_cb,
	            &USR_cb);

	return;
}

/*
 * Call this to indicate a failure.  Blinks the STM32F4 discovery LEDs
 * in sequence.  At 168Mhz, the blinking will be very fast - about 5 Hz.
 * Keep that in mind when debugging, knowing the clock speed might help
 * with debugging.
 */
void ColorfulRingOfDeath(void)
{
	uint16_t ring = 1;
	while (1)
	{
		uint32_t count = 0;
		while (count++ < 500000);

		GPIOD->BSRRH = (ring << 12);
		ring = ring << 1;
		if (ring >= 1<<4)
		{
			ring = 1;
		}
		GPIOD->BSRRL = (ring << 12);
	}
}

/*
 * Interrupt Handlers
 */

void SysTick_Handler(void)
{
	ticker++;
	if (downTicker > 0)
	{
		downTicker--;
	}
}

void NMI_Handler(void)       {}
void HardFault_Handler(void) { ColorfulRingOfDeath(); }
void MemManage_Handler(void) { ColorfulRingOfDeath(); }
void BusFault_Handler(void)  { ColorfulRingOfDeath(); }
void UsageFault_Handler(void){ ColorfulRingOfDeath(); }
void SVC_Handler(void)       {}
void DebugMon_Handler(void)  {}
void PendSV_Handler(void)    {}

void OTG_FS_IRQHandler(void)
{
  USBD_OTG_ISR_Handler (&USB_OTG_dev);
}

void OTG_FS_WKUP_IRQHandler(void)
{
  if(USB_OTG_dev.cfg.low_power)
  {
    *(uint32_t *)(0xE000ED10) &= 0xFFFFFFF9 ;
    SystemInit();
    USB_OTG_UngateClock(&USB_OTG_dev);
  }
  EXTI_ClearITPendingBit(EXTI_Line18);
}
