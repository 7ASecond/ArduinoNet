#include <NewPing.h> // For collision Detection (Ultrasonic)


#define TRIGGER_PIN  11  // Arduino pin tied to trigger pin on the ultrasonic sensor.
#define ECHO_PIN     12  // Arduino pin tied to echo pin on the ultrasonic sensor.
#define MAX_DISTANCE 300 // Maximum distance we want to ping for (in centimeters). Maximum sensor distance is rated at 400-500cm.


const int DirectionX_pin = 4;  // analog pin connected to X output Turn left/Right
const int PitchY_pin = 3;  // analog pin connected to Y output Pitch Up/Down
const int ThrottleY_pin = 5;   // analog pin connected to Y output Accelerate/Decelerate
const int AutomaticLights_pin = 0; // Switch on/off cockpit console lights

NewPing sonar(TRIGGER_PIN, ECHO_PIN, MAX_DISTANCE); // NewPing setup of pins and maximum distance.



void setup() {
  // put your setup code here, to run once:

  pinMode(DirectionX_pin, INPUT);
  pinMode(PitchY_pin, INPUT);
  pinMode(ThrottleY_pin, INPUT);
  pinMode(AutomaticLights_pin, INPUT);

  Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:

  DetectCollision();
  DetectLight();
  DetectDirection();
  DetectPitch();
  DetectThrottle();
}


void DetectCollision()
{

unsigned int uS = sonar.ping(); // Send ping, get ping time in microseconds (uS). 
//Serial.println(uS);
Serial.println(String("CD") + String(uS / US_ROUNDTRIP_CM) + String("Z"));
}

void DetectLight()
{
  Serial.println(String("DL") + String(analogRead(AutomaticLights_pin)) + String("Z"));
}

void DetectDirection()
{
  Serial.println(String("DD") + String(analogRead(DirectionX_pin)) + String("Z"));
}

void DetectPitch()
{
  Serial.println(String("DP") + String(analogRead(PitchY_pin)) + String("Z"));
}

void DetectThrottle()
{
  Serial.println(String("DT") + String(analogRead(ThrottleY_pin)) + String("Z"));

}

