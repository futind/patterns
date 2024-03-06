#include <iostream>
#include <string>
#include <vector>

class IListener {
public:
	virtual void hear(std::string) = 0;
};

class Professor {
	unsigned int id;
	std::vector<std::string> knowledge;
	std::vector<std::string>::iterator it;
public:
	Professor() : id(0), knowledge({}) {};
	Professor(unsigned int ID, std::vector<std::string> Knowledge) : id(ID), knowledge(Knowledge)
	{
		it = knowledge.begin();
	}
	std::string spreadKnowledge()
	{	
		if (it == knowledge.end())
		{
			return "End of lection.";
		}
	}
};

class Classroom {
	unsigned int number;
	Professor P;
	std::vector<Student> listeners; 
	std::string last_phrase;
public:
	void carryTheSound(std::string phrase)
	{
		last_phrase = phrase;
		for (auto& it : listeners)
		{
			it.hear(last_phrase);
		}
	}
	void addListener(Student& L)
	{
		listeners.push_back(L);
	}
	void removeListener(Student &L)
	{
		for (std::vector<Student>::iterator it = listeners.begin(); it != listeners.end(); ++it)
		{
			if (it->showID() == L.showID())
			{
				listeners.erase(it);
				break;
			}
		}
	}

};

class Student : public IListener
{
	unsigned int id;
	std::vector<std::string> notebook;
public:
	Student() : id(0) {};
	Student(unsigned int ID) : id(ID) {};
	unsigned int showID()
	{
		return id;
	}
	void hear(std::string phrase)
	{
		notebook.push_back(phrase);
	}
};

int main()
{
	Professor P(1, { "sentence1", "sentence2", "sentence3" });
	Student A(1);
	Student B(2);
	Student C(3);
	Student D(4);

	Classroom Class();



	return 0;
}