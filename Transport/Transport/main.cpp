#include <iostream>
#include <vector>

class IVehicle
{
public:
	virtual void move(double lat, double long) = 0;
};

class Car : public IVehicle
{
private:
	static unsigned int counter;
	bool keyInIgnition;
	bool isOpen;
	Driver* driver;
	std::vector<Person> passengers;
	double fuelInTank;
	unsigned int key;
	unsigned int capacity;
public:
	Car()
	{
		keyInIgnition = false;
		isOpen = false;
		driver = NULL;
		fuelInTank = 100;
		counter++;
		key = counter;
		capacity = 4;
	}
	void move(Driver D, int latitude, int longitude)
	{
		if (this->driver == NULL)
		{
			std::cout << "Car can not move, there is no driver inside" << std::endl;
			return;
		}
		if (this->driver->showID() == D.showID() && keyInIgnition && fuelInTank > 0)
		{
			std::cout << "Car is moving to the coordinates: " << latitude << ", " << longitude << std::endl;
		}
	}
	void open(Driver D)
	{
		if (this->driver == NULL)
		{
			std::cout << "Car can not be opened, there is no driver assigned" << std::endl;
			return;
		}
		if (this->driver->showID() == D.showID() && this->key == this->driver->putKeyInKeyhole())
		{
			isOpen = true;
			std::cout << "Car is now opened by driver with ID " << D.showID() << std::endl;
		}
	}
	void close(Driver D)
	{
		if (this->driver == NULL)
		{
			std::cout << "Car can not be closed, there is no driver assigned" << std::endl;
			return;
		}
		if (this->driver->showID() == D.showID() && this->key == this->driver->putKeyInKeyhole())
		{
			isOpen = false;
			std::cout << "Car is now closed by driver with ID " << D.showID() << std::endl;
		}
	}
	void start(Driver D)
	{
		if (this->driver == NULL)
		{
			std::cout << "Car can not be started, there is no driver inside" << std::endl;
			return;
		}
		if (this->driver->showID() == D.showID() && this->key == this->driver->putKeyInKeyhole())
		{
			keyInIgnition = true;
			std::cout << "Car is now started by driver with ID " << D.showID() << std::endl;
		}
	}
	void stop()
	{
		keyInIgnition = false;
		std::cout << "Car is stopped now" << std::endl;
	}
	void addDriver(Driver& D)
	{
		driver = &D;
	}
	void removeDriver(Driver& D)
	{
		driver = NULL;
	}
	void addPassenger(Person& P)
	{
		if (passengers.size() == capacity)
		{
			std::cout << "There is no room in this car!" << std::endl;
			return;
		}
		passengers.push_back(P);
	}
	void removePassenger(Person& P)
	{
		for (int i = 0; i < passengers.size(); i++)
		{
			if (passengers[i].showID() == P.showID())
			{
				passengers.erase(passengers.begin() + i);
				std::cout << "Passenger with ID " << P.showID() << " is removed from the car" << std::endl;
				break;
			}
		}
	}
};

unsigned int Car::counter = 0;

class Person
{
private:
	static unsigned int person_counter;
	unsigned int id;
public:
	Person()
	{
		person_counter++;
		id = person_counter;
	}
	unsigned int showID()
	{
		return id;
	}
	void getInCar(Car C)
	{
		C.addPassenger(*this);
	}
	void getOutOfCar(Car C)
	{
		C.removePassenger(*this);
	}
};

unsigned int Person::person_counter = 0;

class Driver : public Person
{
private:
	static unsigned int driver_counter;
	unsigned int id;
	unsigned int key;
public:
	Driver()
	{
		driver_counter++;
		id = driver_counter;
		key = driver_counter;
	}
	void getInCar(Car C)
	{
		C.addDriver(*this);
	}
	void getOutOfCar(Car C)
	{
		C.removeDriver(*this);
	}
	void openCar(Car C)
	{
		C.open(*this);
	}
	void startTheCar(Car C)
	{
		C.start(*this);
	}
	void drive(Car C, int latitude, int longitude)
	{
		C.move(*this, latitude, longitude);
	}
	unsigned int putKeyInKeyhole()
	{
		return key;
	}
};

unsigned int Driver::driver_counter = 0;

int main()
{
	Car A();
	Car B();

	Driver DA;
	Driver DB;
	Person P1, P2, P3, P4, P5, P6;

	DA.openCar(A);
	DA.getInCar(A);
	P1.getInCar(A); P2.getInCar(A); P3.getInCar(A); P4.getInCar(5);
	DA.startTheCar(A);
	DA.drive(A, 1, 2);

	DB.openCar(B);
	DB.getInCar(B);
	P4.getInCar(B); P5.getInCar(B); P6.getInCar(B);
	DB.startTheCar(B);
	DB.drive(B, 3, 4);


	return 0;
}