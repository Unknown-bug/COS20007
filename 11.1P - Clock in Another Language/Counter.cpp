#include <string>

using namespace std;

class Counter {
private:
    int _count;
    string _name;

public:
    Counter(const string& name) : _count(0), _name(name) {}

    void Increment() {
        ++_count;
    }

    void Reset() {
        _count = 0;
    }

    string Name() const {
        return _name;
    }

    void SetName(const string& value) {
        _name = value;
    }

    int Ticks() const {
        return _count;
    }
};
