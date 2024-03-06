#pragma once
#include <typeinfo>
#include <string>

class IListener {
public:
	virtual void hear(const std::string Phrase) = 0;
};