#include <cstdint>
#include <set> 
#include <Adafruit_NeoPixel.h>

#define NEOPIXEL_IN_PIN 13
#define NUM_PIXELS 16

#define JOYSTICK_VERTICAL_PIN 2
#define JOYSTICK_HORIZONTAL_PIN 4
#define JOYSTICK_BUTTON_PIN 0

Adafruit_NeoPixel Neopixel(NUM_PIXELS, NEOPIXEL_IN_PIN, NEO_GRB + NEO_KHZ800);

class IMySubject;

class IMyObserver 
{
public:
  virtual void Update(IMySubject* ChangedSubject) = 0;
};

class IMySubject
{
public:
  virtual void addObserver(IMyObserver* O)
  {
    _observers.insert(O);
  }
  virtual void removeObserver(IMyObserver* O)
  {
    auto found = _observers.find(O);
    if (found != _observers.end())
    {
	    _observers.erase(found);
    } 
  }
  virtual void notifyObservers()
  {
    for (auto& observer : _observers)
    {
	    observer->Update(this);
    }
  }
private:
  std::set<IMyObserver*> _observers;
};

// Joystick is a kind of a sensor, right?
class Joystick : public IMySubject {
  uint16_t horizontal;
  uint16_t vertical;
  int32_t button;
  uint16_t HORIZONTAL_PIN;
  uint16_t VERTICAL_PIN;
  uint16_t BUTTON_PIN;
  std::set<IMyObserver*> observers; 
public:
  Joystick(uint16_t _HORIZONTAL_PIN, uint16_t _VERTICAL_PIN, uint16_t _BUTTON_PIN) 
          : horizontal(2048), 
            vertical(2048), 
            button(HIGH),
            HORIZONTAL_PIN(_HORIZONTAL_PIN),
            VERTICAL_PIN(_VERTICAL_PIN),
            BUTTON_PIN(_BUTTON_PIN)
  {
    pinMode(HORIZONTAL_PIN, INPUT);
    pinMode(VERTICAL_PIN, INPUT);
    pinMode(BUTTON_PIN, INPUT_PULLUP);
  }
  void readData()
  {
    int new_horizontal = analogRead(HORIZONTAL_PIN);
    int new_vertical = analogRead(VERTICAL_PIN);
    int32_t new_button = digitalRead(BUTTON_PIN);

    if (new_horizontal != horizontal || new_vertical != vertical || button != new_button)
    {
      horizontal = new_horizontal;
      vertical = new_vertical;
      button = new_button;
      notifyObservers();
    }
  }
  
  uint16_t getHorizontal()
  {
    return horizontal;
  }

  uint16_t getVertical()
  {
    return vertical;
  }

  int32_t getButton()
  {
    return button;
  }
};

class LEDStrip : public IMyObserver {
  Adafruit_NeoPixel LED;
  uint16_t number_of_pixels;
  uint16_t LED_PIN;
  Joystick* j_stick;
public:
  LEDStrip(uint16_t _number_of_pixels, uint16_t _LED_PIN, Joystick* j) :
          number_of_pixels(_number_of_pixels), 
          LED_PIN(_LED_PIN),
          j_stick(j)
  {
    pinMode(LED_PIN, OUTPUT);
    Adafruit_NeoPixel Neopixel(number_of_pixels, LED_PIN, NEO_GRB + NEO_KHZ800);
    j_stick->addObserver(this);
    Neopixel.begin();
    Neopixel.clear();
  }
  void Update(IMySubject* ChangedSubject)
  {
    Neopixel.clear();

    if (j_stick->getHorizontal() == 4095)
    {
      // top-left
      if (j_stick->getVertical() == 4095) 
      {
        Neopixel.setPixelColor(14, Neopixel.Color(255, 0, 0));
      }
      // left
      else if (j_stick->getVertical() == 2048)
      {
        Neopixel.setPixelColor(12, Neopixel.Color(255, 0, 0));
      }
      // bottom-left
      else 
      {
        Neopixel.setPixelColor(10, Neopixel.Color(255, 0, 0));
      }
    }
    //right
    else if (j_stick->getHorizontal() == 0)
    {
      // top-right
      if (j_stick->getVertical() == 4095)
      {
        Neopixel.setPixelColor(2, Neopixel.Color(255, 0, 0));
      }
      // right
      else if (j_stick->getVertical() == 2048)
      {
        Neopixel.setPixelColor(4, Neopixel.Color(255, 0, 0));
      }
      // bottom-right
      else 
      {
        Neopixel.setPixelColor(6, Neopixel.Color(255, 0, 0));
      }
    }
    // middle
    else
    {
      // top
      if (j_stick->getVertical() == 4095)
      {
        Neopixel.setPixelColor(0, Neopixel.Color(255, 0, 0));
      }
      // center
      else if (j_stick->getVertical() == 2048)
      {
        Neopixel.clear();
      }
      // bottom
      else
      {
        Neopixel.setPixelColor(8, Neopixel.Color(255, 0, 0));
      }
    }

    if (j_stick->getButton() == LOW)
    {
      for(int pix = 0; pix < number_of_pixels; ++pix)
      {
        Neopixel.setPixelColor(pix, Neopixel.Color(255, 0, 0));
      }
    }

    Neopixel.show();
  }
};

  Joystick Stick(JOYSTICK_HORIZONTAL_PIN, JOYSTICK_VERTICAL_PIN, JOYSTICK_BUTTON_PIN);
  LEDStrip Lamp(NUM_PIXELS, NEOPIXEL_IN_PIN, &Stick);

void setup() {
  Serial.begin(115200);
}

void loop() {
  Stick.readData();
  delay(2000);
}
