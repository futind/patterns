#pragma once
#include "Student.h"
#include "Classroom.h"
#include <string>
#include <vector>

class Professor {
	//std::string name; // Имя лектора
	//std::string surname; // Фамилия лектора
	unsigned int id; // Уникальный идентификационный номер лектора
	std::vector<std::string> knowledge; // Материал лекции
	//std::vector<std::string>::iterator next_sentence; // Следующее предложение, которое говорит лектор
	std::vector<Student> listeners;
public:
	Professor();
	Professor(unsigned int id, std::string name, std::string surname, std::vector<std::string> knowledge);
	Professor(Professor& P);
	void endLection(Classroom C); // По окончании материала лектор удаляет всех из аудитории.
	std::string speak(); // Лектор говорит следующее предложение
	void letStudentInClass(Student S, Classroom C); // Лектор пускает студента в аудиторию
	void letStudentOutOfClass(Student S, Classroom C); // Лектор выпускает студента из аудитории
};