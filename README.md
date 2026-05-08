🎮 Gesture-Controlled Game Controller
Embedded Systems Project — Control games with your hand movements using an MPU6050 sensor and Arduino Nano.


📖 Overview
This project is a motion-based game control system that detects hand movements and converts them into in-game commands in real time. An MPU6050 IMU sensor continuously measures tilt and acceleration data, which is processed by an Arduino Nano and sent to a computer via USB (Serial). On the PC side, a Unity game reads the serial data and applies it directly as player input.

When you tilt the device left or right, the character moves accordingly. A sudden shake triggers an action like an attack or jump.


✨ Features
📐 Real-time tilt detection — Roll and pitch angles mapped to directional movement
💥 Shake gesture detection — Sudden acceleration (>2.5G) triggers in-game actions
🔘 Physical button support — D7 pin for interactions, item pickups, etc.
⚙️ Auto-calibration on startup — Averages 100 sensor readings to set a flat reference
🔌 Wired USB communication — Reliable Serial connection at 9600 baud
🎮 Unity integration — Ready-made C# scripts for serial reading and player control


🛠️ Hardware Requirements
Component
Qty
Purpose
Arduino Nano (Clone)
1
Microcontroller — reads sensor, sends data via USB
MPU6050 GY-521
1
IMU sensor — measures acceleration and gyroscope
Breadboard (830 pin)
1
Solderless prototyping
Jumper Wires (M-F)
1 set
Connections between sensor and Arduino
USB Cable
1
Power + Serial data to PC



🔌 Wiring Diagram
The MPU6050 communicates over I2C — only 4 wires needed.

MPU6050 Pin
Arduino Nano Pin
Description
VCC
5V
Power
GND
GND
Ground
SCL
A5
I2C Clock
SDA
A4
I2C Data
INT
D2
Interrupt (optional)


Button (optional): Connect one leg to pin D7, other leg to GND.


💻 Software Requirements
Arduino Side
Arduino IDE
MPU6050_light library — install via Library Manager
Unity Side
Unity 2021+ (any version supporting serial communication)
SerialController.cs — reads incoming serial data
GesturePlayerController.cs — applies commands to the player character


🚀 Getting Started
1. Flash the Arduino
Install the MPU6050_light library via Arduino IDE → Library Manager
Open gesture_controller.ino
Connect your Arduino Nano via USB
Select the correct Board (Arduino Nano) and Port in the IDE
Upload the sketch

On startup, the Arduino will calibrate automatically — keep the device flat and still for ~1 second.
2. Verify Serial Output
Open the Serial Monitor (9600 baud). You should see angle values:

MPU6050 durumu: 0

Kalibrasyon başlıyor, sensörü oynatma...

Kalibrasyon bitti!

X: 0.12    Y: -0.34    Z: 0.05

X: 0.11    Y: -0.33    Z: 0.04
3. Set Up Unity
Add SerialController.cs and GesturePlayerController.cs to your Unity project
Attach SerialController to an empty GameObject, set the correct COM port
Attach GesturePlayerController to your player object
Press Play — tilt to move!


🕹️ Gesture Commands
Command
Condition
In-Game Effect
LEFT
Roll < -15°
Character moves left
RIGHT
Roll > +15°
Character moves right
FORWARD
Pitch > +15°
Character moves forward
BACK
Pitch < -15°
Character moves backward
SHAKE
Total accel > 2.5G
Attack / jump triggered
BUTTON
D7 pin LOW
Interaction / item pickup
IDLE
None of above
Character stops


The gesture check runs every 50ms (20 Hz). The ±15° threshold is adjustable in code — lower it for sensitive control, raise it to reduce accidental triggers.


🧠 How It Works
Hand Movement

     │

     ▼

MPU6050 Sensor

 (Accel + Gyro)

     │

     ▼

Arduino Nano

 - Calibration on boot

 - Roll / Pitch calculation

 - Gesture thresholding

 - Serial.print("LEFT" / "RIGHT" / ...)

     │

  USB Cable

     │

     ▼

Unity (PC)

 SerialController.cs  →  GesturePlayerController.cs

 (reads port)             (moves the character)
Roll & Pitch
Raw accelerometer values are converted to angles:

Roll = left/right tilt → controls X-axis movement
Pitch = forward/backward tilt → controls Z-axis movement
Shake Detection
If the combined acceleration magnitude exceeds 2.5G, a SHAKE command is fired, triggering attack or jump actions in-game.


📁 Project Structure
gesture-game-controller/

├── gesture_controller.ino       # Arduino firmware

├── Unity/

│   ├── SerialController.cs      # Serial port reader

│   └── GesturePlayerController.cs  # Player movement logic

├── docs/

│   └── gesture_controller_infograph.jpeg

└── README.md


🔧 Troubleshooting
Problem
Solution
MPU6050 durumu: 1 (not 0)
Check SDA/SCL wiring; make sure VCC is 5V
No Serial output
Verify COM port and baud rate (9600)
Character drifts without moving
Re-calibrate: reset Arduino while device is flat
Unity can't open port
Close Serial Monitor in Arduino IDE first
Gestures too sensitive / not sensitive enough
Adjust the 15 degree threshold in .ino



🤝 Contributing
Pull requests are welcome! Ideas for improvement:

Wireless version using ESP8266/ESP32 + WiFi
Bluetooth mode (no USB required)
Configurable gesture profiles
Python receiver script as an alternative to Unity


📄 License
This project is open source and available under the MIT License.


Made with ❤️ as an Embedded Systems project

