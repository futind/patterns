#pragma once
#include "IListener.h"
#include <typeinfo>
#include <vector>
#include <string>
#include <iostream>

class Student: public IListener{
	static unsigned int counter;
	unsigned int id; // Уникальный идентификационный номер студента
	std::string name; // Имя студента
	std::string surname; // Фамилия студента
	std::string group; // Группа студента
	std::vector<std::string> notebook; // Тетрадь, куда студент записывает слова лектора

	void write(std::string phrase); // Студент записывает слова лектора в тетрадь.
public:
	Student();
	Student(std::string name, std::string surname, std::string group);
	Student(const Student& S);
	unsigned int getID();
	void hear(std::string phrase); // Студент слушает лектора
	void showNotebook(); // Студент может показать тетрадь
};