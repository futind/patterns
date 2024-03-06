#include "Classroom.h"

unsigned int counter = 0;

Classroom::Classroom()
{
	number = counter++ + 1;
	capacity = default_capacity;

}

unsigned int Classroom::getNumber()
{
	return number;
}

unsigned int Classroom::getCapacity()
{
	return capacity;
}

unsigned int Classroom::getCurrentCapacity()
{
	return students_in_class.size();
}

void Classroom::addStudent(Student S)
{
	if (students_in_class.size() < capacity) students_in_class.push_back(S);
	else std::cout << "Classroom is full." << std::endl;
}

void Classroom::removeStudent(Student S)
{
	for (auto it = students_in_class.begin(); it != students_in_class.end(); ++it)
	{
		if (it->getID() == S.getID())
		{
			students_in_class.erase(it);
			break;
		}
	}
}

void Classroom::forceEveryoneOut()
{
	students_in_class.clear();
}