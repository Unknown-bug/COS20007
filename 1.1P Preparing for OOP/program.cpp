#include <iostream>
using namespace std;
 
double Average(int arr[], int size) {
    int sum = 0;
    for (int i = 0; i < size; i++) {
        sum += arr[i];
    }
    return static_cast<double>(sum) / size;
}
 
int main() {
    int numbers[] = {10, 20, 30, 40, 50};
    int size = sizeof(numbers) / sizeof(numbers[0]);
 
    double avg = Average(numbers, size);
 
    cout << "The average is: " << avg << endl;
 
    if (avg >= 10) {
        cout << "Double digits" << endl;
    } else {
        cout << "Single digits" << endl;
    }
 
    if (avg < 0) {
        cout << "Average value is in the negative" << endl;
    }
 
    return 0;
}
