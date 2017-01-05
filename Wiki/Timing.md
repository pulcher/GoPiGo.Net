# Timing

Because the GoPiGo uses a serial bus (I2C), it is important to note that timing is critical!

Depending on the action you would like to execute, you need to wait a few milliseconds.

On average, you need for a *write* action minimal 7ms.
For a *write* followed by a *read* you need again 7ms in between.
If you are reading only one byte, you don't need a wait, but if you need two bytes,
an average of 5ms is needed.

These timings can vary in various cases and are measured on speed setting 'normal'.

Note that some actions take some processing time for the GoPiGo. In this case you need to add the processing time with the default waiting time.

Best is to take the waiting time, used by the Python version (here: https://github.com/DexterInd/GoPiGo/blob/master/Software/Python/gopigo.py) and try to find a smooth ms for the UWP version.


To wait in UWP, you can use the following snippet:
```Cs
	Task.Delay(100).Wait();	
	//Example for a write followed by a read of a single byte
	var buffer = new[] { (byte)Commands.DigitalRead, (byte)pin, Constants.Unused, Constants.Unused };
    WriteToI2C(buffer);
	Task.Delay(100).Wait();
	var data = ReadByte();
```

## Using GoPiGo.Net Library
Most methods handle the timing between actions, like the example above, but not before!
Let's use the following example:
```Cs
	gopigo.MotorController().MoveForward();
	gopigo.MotorController().Stop();
```
When the Stop method is called, there is no delay between the last I2C Command and the next one. So it is important to add a delay between both method calls:
```Cs
	gopigo.MotorController().MoveForward();
	await Task.Delay(100);
	gopigo.MotorController().Stop();
```
In the example here, we've used 100ms. It is *adviced* to use *at least* 7ms between method calls.
