#include "Professor.h"

Professor::Professor()
{
	name = "Name";
	surname = "Surname";
	id = 999;
	knowledge = { "OOP", "Patterns", "UML" };
	next_sentence = knowledge.begin();
	listeners = {};
}

std::string Professor::speak()
{
	if (next_sentence != knowledge.end())
	{
		std::string sentence = *next_sentence;
		next_sentence++;
		return sentence;
	}
	return "End of the lection.\n";
}

void Professor::endLection(Classroom C)
{
	C.forceEveryoneOut();
}

void Professor::letStudentInClass(Student S, Classroom C)
{
	C.addStudent(S);
}

void Professor::letStudentOutOfClass(Student S, Classroom C)
{
	C.removeStudent(S);
}