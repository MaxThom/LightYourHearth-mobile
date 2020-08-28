# Project LightYourHearth-mobile
The goal of this project is to connect a rpi with ledstrips with individually adressable leds.
It will then be controllable with a bluetooth connection using a phone app. Enjoy :)

# Motivation
The motivation behind this project is too decorate our homes, bicyles and anywhere you can think of with awesome controllable led.
Moreover, the second goal is too learn how to make an electronic project, how to use bluetooth and how to connect everything through an app.

# Technologies

### RPI
The bluetooth server and ledstrip connection on the rpi are made with Python3. I use Snake Case as code style.

##### Hardware
 - Raspberry pi zero w
 - WS2812B Led strip
 - Electric cables
 - 5 volt power supply with 10 amps
 - 2 small buttons

### Phone App
The phone app used to connect and control the ledstrip is made with Xamarin. I use C# convention as Code Style.

# Features
- Being able to control the device using a phone app and without internet connection
- Being plug and play as much as possible
- User will be able to select a bunch of animations
- User will be able to create an animation playlist with loop or not
- User will be able to divide the strip in different section and target those sections individually with animations

# How to use
#### **Commands for Systemd**
 - sudo nano /lib/systemd/system/lightyourhearth.service 
 - sudo chmod +x /lib/systemd/system/lightyourhearth.service
 - sudo systemctl daemon-reload
 - sudo systemctl enable lightyourhearth.service
 - sudo systemctl status lightyourhearth.service

#### **Command for Cronjobs**
 - sudo crontab -e
 - sudo crontab -l

# Installation
 #### Rpi
 1. Enable SSH, VNC and SPI in interfaces configuration
 2. Install bluetooth components
    - sudo apt-get install bluetooth bluez blueman libbluetooth-dev
    - sudo apt-get install python-bluetooth
 3. Add SP profile to bluetooth
    - sudo nano /etc/systemd/system/dbus-org.bluez.service
    - A file will open and add the compatibility flag, ' -C', at the end of the 'ExecStart=' line. Add a new line after that to add the SP profile. The two lines should look like this:
        - ExecStart=/usr/lib/bluetooth/bluetoothd -C
        - ExecStartPost=/usr/bin/sdptool add SP
 4. Download repo to Desktop Folderand navigate to root folder. Make sure it is in this path */home/pi/Desktop/LightYourHearth-rpi* 
 5. Install requirements : sudo pip3 install -r requirements.txt
 6. Add LightYourHearth-Update script to crontab
    - Open terminal and type : sudo crontab -e
    - Add this line to start server update check on system boot : **@reboot sh /home/pi/Desktop/LightYourHearth-rpi/LightYourHearth-Update.sh >/home/pi/Desktop/LightYourHearth-rpi/Logs/cronlogs 2>&1**
    - Save, you can confirm using sudo crontab -l
 7. Add Systemd unit file to systemd deamon
    - sudo cp lightyourhearth.service /lib/systemd/system/
    - sudo chmod +x /lib/systemd/system/lightyourhearth.service
    - sudo systemctl daemon-reload
    - sudo systemctl enable lightyourhearth.service
    - sudo systemctl status lightyourhearth.service

# Credits
n/a

# Liscense
MIT

# RoadMap
*August 2020*
- [x] RPI - Create the device with the rpi, wires, ledstrip and power supply
- [x] RPI - Control the ledstrip using a python script
- [x] RPI - Add a bluetooth server to communicate with a phone
- [x] RPI - Configure Raspbian to autolaunch the script on bootup
- [x] RPI - Auto update the bluetooth server from git
- [ ] RPI - Remove bluetooth security when pairing a device
- [x] APP - Create a phone app using Xamarin and learn the basics
- [x] APP - Add bluetooth client connection
- [x] APP/RPI - Add handshake when initializing connections to discover servers capabilities (animations, etc)
- [x] APP - Create a dynamic UI for animations

*September 2020*
- [ ] APP - Create playlist for animations
- [ ] APP - Create sections on the ledstrip for animations to target those sections
- [ ] RPI - Add more animations
- [X] RPI - Add different ledstrip type (SK6812)


