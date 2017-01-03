# Assembling GoPiGo Robot

Assembling the Robot contains out of two parts
- Hardware
- Software

## Hardware

Follow the official Steps provided by Dexter industries:
https://www.dexterindustries.com/GoPiGo/getting-started-with-your-gopigo-raspberry-pi-robot-kit-2/1-assemble-the-gopigo-2/assemble-gopigo-raspberry-pi-robot/

## Software
Concerning Software running on the GoPiGo, we divert away from the official tutorial:
### Windows 10 IOT core
	We need to install Windows 10 IOT on the SD card:
	
	1) Insert the SD card into the PC and open your Windows IOT Dashboard
	2) Goto the Set up a new device
		- Select raspberry pi 2 & 3 for the device type
		- Select the latest verion
		- Select the SD card 
		- Give it the name (example: your awesome team name)
		- Use a password which you don't forget the next 24h
	3) Accept the terms and download
	4) Wait till it completes the installation and insert it into the assembled Bot.
	5) Connect the raspberry pi via the Ethernet cable provided in the kit with your PC. 
	6) Power the raspberry pi via the USB board and wait a few moments
	If you see something on the HDMI output, you are fine to continue!
	Else, unplug the power and plug it in again. Sometimes Windows takes some time...
	
	7) Open the browser and navigate to http://[YOURROBOTNAME]:8080. Use administrator followed by the password you've entered in step 2.
	8) //TODO Debug pin
	9) //TODO Access the Wifi credentials

Now your bot is connected to the wifi, Try to deploy the test app and watch what the robot does.