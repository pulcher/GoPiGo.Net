# Assembling GoPiGo Robot

Assembling the robot consists of:
1. Assembling the GoPiGo **hardware** parts and the Raspberry Pi.
2. Installing **software** on a SD card that can be inserted into the Raspberry Pi.

## Hardware

To assemble the GoPiGo robot and the Raspberry Pi, follow the official steps provided by Dexter industries:
https://www.dexterindustries.com/GoPiGo/getting-started-with-your-gopigo-raspberry-pi-robot-kit-2/1-assemble-the-gopigo-2/assemble-gopigo-raspberry-pi-robot/

## Software

We provide custom steps for installing software. The following instructions do not reflect the official instructions.

### Windows 10 IoT Core

The following steps will guide you through installing Windows 10 IoT Core on your SD card.

1. Insert the SD card into a PC and open the *Windows 10 IoT Core Dashboard* app.
2. 
2. Click on *Set up a new device* and enter the following values:

	* **Device type**: Raspberry Pi 2 & 3

	* **OS Build**: Windows 10 IoT Core

	* **Drive**: (select your SD card)

	* **Device name**: (your favorite dessert or whatever)

	* **Administrator password**: (enter a password (don't forget it))

3. Accept the terms and press *Download and install*.

4. When the installation is completed, insert the SD card into the Rasberry Pi.

5. Use the Ethernet cable provided in your kit to connect the Raspberry Pi to your PC.

6. Power up the Raspberry Pi using the USB board and wait for it to start.
	
    - If the Raspberry Pi is starting (check using the HDMI output), continue to the next step.
	- Else, unplug the power and plug it in again.

7. Open the browser and navigate to *http://[YOURROBOTNAME]:8080*. Use administrator as username and the password used in step 2.

8. On the first screen you see, Enter a 4 digit number in the textbox associated to it. Donot forget this pin as you will need it while running the app.

9. Open the **Networking** tab, reload the wifi networks, Select a network, enter the password below an hit Connect. You might need to reload the page.

You can now disconnect the ethernet cable.