# Project LightYourHearth-mobile
The goal of this project is to connect a rpi with ledstrips with individually adressable leds.
It will then be controllable with a bluetooth connection using a phone app.

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
 - 5 volt power supply
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
n/a

# Installation
n/a

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
- [ ] RPI - Remove bluetooth security when pairing a device
- [x] APP - Create a phone app using Xamarin and learn the basics
- [x] APP - Add bluetooth client connection
- [ ] APP/RPI - Add handshake when initializing connections to discover servers capabilities (animations, etc)
- [ ] APP - Create a dynamic UI for animations

*September 2020*
- [ ] APP - Create playlist for animations
- [ ] APP - Create sections on the ledstrip for animations to target those sections
- [ ] RPI - Add more animations
- [ ] RPI - Add different ledstrip type (SK6812)
- [ ] RPI - Create a docker container for the server
- [ ] RPI - Create a script to update the container on launch if connected to wifi or cable


