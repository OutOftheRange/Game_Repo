using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomanNumbersConverter : MonoBehaviour
{
    public string Roman;

    public int RomanToInt(string romanNumber)
    {
        int countOfDigits = 0;
        int sum = 0;
        Dictionary<char, int> romanNumbersDictionary = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
        for (int i = 0; i < romanNumber.Length; i++)
        {
            char currentRomanChar = romanNumber[i];
            romanNumbersDictionary.TryGetValue(currentRomanChar, out int num);

            for(int j = 0; j < romanNumber.Length; j++){
                if (romanNumber[i] == romanNumber[j]) countOfDigits++;
            }
            if (i + 3 < romanNumber.Length &&
                currentRomanChar == romanNumber[i+1] &&
                currentRomanChar == romanNumber[i+2] &&
                currentRomanChar == romanNumber[i+3]) {
                Debug.Log("Break the Building1");
                HPLogic.Instance.TakeDamage();
                return 0;
            }
            else if ( i + 2 < romanNumber.Length &&
                    romanNumbersDictionary[romanNumber[i + 1]] > romanNumbersDictionary[currentRomanChar] &&
                    romanNumbersDictionary[romanNumber[i + 2]] > romanNumbersDictionary[currentRomanChar]){
                Debug.Log("Break the Building2");
                HPLogic.Instance.TakeDamage();
                return 0;
            }
            else if ( i + 2 < romanNumber.Length &&
                    romanNumbersDictionary[romanNumber[i + 1]] > romanNumbersDictionary[currentRomanChar] &&
                    romanNumbersDictionary[romanNumber[i + 2]] == romanNumbersDictionary[currentRomanChar]){
                Debug.Log("Break the Building3");
                HPLogic.Instance.TakeDamage();
                return 0;
            }
            else if (i + 1 < romanNumber.Length && romanNumbersDictionary[romanNumber[i + 1]] > romanNumbersDictionary[currentRomanChar])
            {
                switch (currentRomanChar.ToString())
                {
                    case "I" when romanNumber[i + 1].Equals('V') || romanNumber[i + 1].Equals('X'):
                        sum -= num;
                        break;
                    case "X" when romanNumber[i + 1].Equals('L') || romanNumber[i + 1].Equals('C'):
                        sum -= num;
                        break;
                    case "C" when romanNumber[i + 1].Equals('D') || romanNumber[i + 1].Equals('M'):
                        sum -= num;
                        break;
                    default:
                        Debug.Log("Break the Building4");
                        HPLogic.Instance.TakeDamage();
                        return 0;
                }
            }
            else
            {
                sum += num;
            }
            countOfDigits = 0;
        }
        return sum;
    }
}
