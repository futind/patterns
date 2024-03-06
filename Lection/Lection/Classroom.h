#pragma once
#include <vector>
#include "IListener.h"
#include "Student.h"
#include "Professor.h"

class Classroom {
	const static unsigned int default_capacity = 40;
	static unsigned int counter;
	unsigned int number;
	unsigned int capacity;
	Professor professor;
	std::vector<Student> students_in_class;
public:
	Classroom();
	Classroom(unsigned int number, unsigned int capacity);
	unsigned int getCurrentCapacity();
	unsigned int getNumber();
	unsigned int getCapacity();
	void addStudent(Student S);
	void removeStudent(Student S);
	void forceEveryoneOut();
};