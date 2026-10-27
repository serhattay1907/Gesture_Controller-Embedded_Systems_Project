#include "Wire.h"
#include <MPU6050_light.h>

MPU6050 mpu(Wire);
unsigned long timer = 0;

void setup() {
  Serial.begin(9600);
  Wire.begin();
  byte status = mpu.begin();
  while (status != 0) { }
  delay(1000);
  mpu.calcOffsets();
}

void loop() {
  mpu.update();
  if ((millis() - timer) > 50) {
    float roll  = mpu.getAngleX();
    float pitch = mpu.getAngleY();

    if      (roll  < -15.0) Serial.println("LEFT");
    else if (roll  >  15.0) Serial.println("RIGHT");
    else if (pitch >  15.0) Serial.println("FORWARD");
    else if (pitch < -15.0) Serial.println("BACK");
    else                    Serial.println("IDLE");

    timer = millis();
  }
}