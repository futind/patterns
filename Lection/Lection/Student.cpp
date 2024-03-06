#include "Student.h"

unsigned int Student::counter = 0;

Student::Student()
{
	name = "Name";
	surname = "Surname";
	group = "default_group";
	id = counter++ + 1;
}

Student::Student(std::string name, std::string surname, std::string group) : 
				 name(name), surname(surname), group(group)
{
	id = counter++ + 1;
}

Student::Student(const Student& S) : name(S.name), surname(S.surname), group(S.group)
{
	id = counter++ + 1;
}

unsigned int Student::getID()
{
	return id;
}

void Student::hear(std::string Phrase)
{
	write(Phrase);
}

void Student::write(std::string Phrase)
{
	notebook.push_back(Phrase);
}

void Student::showNotebook()
{
	for (int i = 0; i < notebook.size(); i++)
	{
		std::cout << notebook[i] << std::endl;
	}
}