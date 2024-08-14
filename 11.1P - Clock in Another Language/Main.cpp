#include <bits/stdc++.h>
#include <thread>
#include <chrono>

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

class Clock {
private:
    Counter _seconds;
    Counter _minutes;
    Counter _hours;

public:
    Clock() : _seconds("seconds"), _minutes("minutes"), _hours("hours") {}

    void Tick() {
        _seconds.Increment();
        if (_seconds.Ticks() > 59) {
            _minutes.Increment();
            _seconds.Reset();
            if (_minutes.Ticks() > 59) {
                _hours.Increment();
                _minutes.Reset();
                if (_hours.Ticks() > 23) {
                    Reset();
                }
            }
        }
    }

    void Reset() {
        _seconds.Reset();
        _minutes.Reset();
        _hours.Reset();
    }

    string Time() const {
        ostringstream oss;
        oss << setw(2) << setfill('0') << _hours.Ticks() << ":"
            << setw(2) << setfill('0') << _minutes.Ticks() << ":"
            << setw(2) << setfill('0') << _seconds.Ticks();
        return oss.str();
    }
};

int main() {
    Clock clock;

    for (int x = 0; x < 86400; ++x) {
        clock.Tick();
        cout << clock.Time() << endl;
        _sleep(100);
        // std::this_thread::sleep_for(chrono::seconds(1));
        // system("clear");
    }

    return 0;
}
