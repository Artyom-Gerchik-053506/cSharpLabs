// ---------------------------------------------------------------------------

#pragma hdrstop

#include "DLL.h"
#include <stdlib.h>
#include <iostream>
#include <time.h>
#include <iomanip>
// ---------------------------------------------------------------------------
#pragma package(smart_init)

using namespace std;

// float addition(float a, float b) {
// return a + b;
// }

void myMatrix(int rows, int collums) {
	srand(time(NULL));

	int **array = new int * [rows];

	int mainSum = 0;

	for (int i = 0; i < rows; i++) {
		array[i] = new int[collums];
	}

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < collums; j++) {
			array[i][j] = rand() % 10 + 1;
		}
	}

	cout << "Yours Matrix: " << endl;

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < collums; j++) {
			cout << setw(4) << array[i][j];
		}
		cout << endl;
	}

	cout << endl << "Elements On The Main Diagonal: ";

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < collums; j++) {
			if (i == j) {
				cout << setw(4) << array[i][j];
				mainSum += array[i][j];
			}
		}
	}
	cout << endl;
	cout << "Their Sum Is: " << mainSum << endl;

	for (int i = 0; i < rows; i++) {
		delete[]array[i];
	}
	delete[]array;

}
