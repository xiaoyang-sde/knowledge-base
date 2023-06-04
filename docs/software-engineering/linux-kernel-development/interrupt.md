# Interrupt

The kernel needs to communicate with the machine's individual devices. Instead of issuing a request and wait for a response from the hardware, the hardware sends an interrupt to signal to the processor, and the processor signals the kernel. Each interrupt has a unique interrupt request line with an assigned numeric value.

## Interrupt Handler

The function the kernel runs in response to a specific interrupt is called an interrupt handler. Each device that generates interrupts has an associated interrupt handler. The interrupt handler for a device is part of the device's driver.

In Linux, interrupt handlers are normal functions that matches a specific interface, which enables the kernel to pass the handler information. Because an interrupt handler should be efficient and might perform a large amount of work, the processing of interrupts is split into two halves. The interrupt handler is the top half and performs the time-critical work. Work that can be performed later is deferred until the bottom half.

The device driver could register an interrupt handler with `request_irq()`. `irq` specifies the interrupt number, `handler` is a pointer to the interrupt handler, and `dev` is an optional pointer to the driver's device structure.

```c
typedef irqreturn_t (*irq_handler_t)(int, void *);

int request_irq(
  unsigned int irq,
  irq_handler_t handler,
  unsigned long flags,
  const char *name,
  void *dev
);
```

- `IRQF_DISABLED` disables all interrupts when executing this interrupt handler, which is reserved for performance-sensitive interrupts.
- `IRQF_SAMPLE_RANDOM` specifies that interrupt should contribute to the kernel entropy pool. The kernel entropy pool provides random numbers derived from various random events.
- `IRQF_TIMER` specifies that this handler processes interrupts for the system timer.
- `IRQF_SHARED` specifies that the interrupt line can be shared among multiple interrupt handlers.

The interrupt handler returns either `IRQ_NONE` or `IRQ_HANDLED`. `IRQ_NONE` is returned when the interrupt handler detects an interrupt for which its device was not the originator. `IRQ_HANDLED` is returned if the interrupt handler was invoked, and its device did indeed cause the interrupt.

Multiple interrupt handlers can share the same interrupt line when multiple devices are configured to generate interrupts with the same interrupt number. The `IRQF_SHARED` flag is set in `request_irq()` and the `dev` argument must be unique to each registered handler. The interrupt handler must be capable of distinguishing whether its device generated an interrupt. When the kernel receives an interrupt, it invokes each registered handler on the line.

When executing an interrupt handler, the kernel is in interrupt context. Without a backing process, interrupt context can't sleep. Each interrupt handler has a stack with a size of a page. After the interrupt handler returns, the kernel should continue to execute kernel-space or user-space code. If the kernel is returning to the user-space, `schedule()` is called. If the kernel is returning to the kernel-space, `schedule()` is called if `preempt_count` is `0`.

## Interrupt Control

The kernel implements interfaces for manipulating the state of interrupts on a machine. The interfaces allow to disable the interrupt system for the current processor or mask out an interrupt line for the entire machine. Disabling interrupts provides protection against concurrent access from a possible interrupt handler.
