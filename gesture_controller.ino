#include "Wire.h"
#include <MPU6050_light.h>

MPU6050 mpu(Wire);
unsigned long timer = 0;

void setup() {
  Serial.begin(9600);
  Wire.begin();
  
  byte status = mpu.begin();
  Serial.print(F("MPU6050 durumu: "));
  Serial.println(status);
  
  // Sensörün hata verip vermediğini kontrol et
  while(status != 0){ } 
  
  Serial.println(F("Kalibrasyon başlıyor, sensörü oynatma..."));
  delay(1000);
  mpu.calcOffsets(); // Jiroskop ve ivmeölçer ofsetlerini hesaplar
  Serial.println(F("Kalibrasyon bitti!\n"));
}

void loop() {
  mpu.update();
  
  // Verileri her 100ms'de bir ekrana bas
  if((millis() - timer) > 100) { 
    Serial.print("X: ");  Serial.print(mpu.getAngleX());
    Serial.print("\tY: "); Serial.print(mpu.getAngleY());
    Serial.print("\tZ: "); Serial.println(mpu.getAngleZ());

    timer = millis();  
  }
}