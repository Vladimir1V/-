using System;

namespace Program2
{
    public class MyString
    {
        public char[] Value => _str;
        public char[] Empty => _empty();

        private char[] _str;

        public MyString(char[] array)
        {
            _str = new char[array.Length];
            array.CopyTo(_str, 0);
        }

        public MyString(string array)
        {
            _str = array.ToCharArray();
        }

        public MyString(int length)
        {
            _str = new char[length];
        }

        #region override+
        public static MyString operator +(MyString firstValue, MyString secondValue)
        {
            firstValue.AddToMyString(secondValue);
            return firstValue;
        }
        public static MyString operator +(MyString firstValue, char[] secondValue)
        {
            firstValue.AddToMyString(secondValue);
            return firstValue;
        }
        public static MyString operator +(MyString firstValue, string secondValue)
        {
            firstValue.AddToMyString(secondValue.ToCharArray());
            return firstValue;
        }
        #endregion

        #region override<>
        public static bool operator <(MyString firstValue, MyString secondValue) => firstValue.ToInt32() < secondValue.ToInt32();
        public static bool operator <(MyString firstValue, char[] secondValue) => firstValue.ToInt32() < secondValue.Length;

        public static bool operator >(MyString firstValue, MyString secondValue) => firstValue.ToInt32() > secondValue.ToInt32();
        public static bool operator >(MyString firstValue, char[] secondValue) => firstValue.ToInt32() > secondValue.Length;
        #endregion

        public int ToInt32() => _str.Length;

        public MyString Substring(int startNumber)
        {
            char[] result = new char[Value.Length - (Value.Length - startNumber)];
            var index = 0;
            do
            {
                result[index] = Value[startNumber];
                index++;
                startNumber++;
            } while (startNumber != Value.Length);
            return new MyString(result);
        }

        public MyString Substring(int startNumber, int endNumber)
        {
            if (endNumber > Value.Length - 1) return null;
            var index = 0;
            char[] result = new char[endNumber - startNumber + 1];
            do
            {
                result[index] = Value[startNumber];
                startNumber++;
                index++;
            } while (startNumber != endNumber + 1);
            return new MyString(result);
        }

        public MyString Copy() => new MyString(Value);

        public static void Copy(MyString stringFromCopy, ref MyString stringToCopy) => stringToCopy = new MyString(stringFromCopy.Value);

        public int? IndexOf(char symb)
        {
            for (int i = 0; i < _str.Length; i++)
            {
                if (_str[i] == symb) return i;
            }

            return null;
        }

        public int? IndexOf(char symb, int startNumber)
        {
            for (int i = startNumber; i < _str.Length; i++)
            {
                if (_str[i] == symb) return i;
            }

            return null;
        }

        public void Replace(char replaceFor, char replaceTo)
        {
            for (int i = 0; i < _str.Length; i++)
            {
                if (_str[i] == replaceFor) _str[i] = replaceTo;
            }
        }

        public void Replace(string replaceFor, string replaceTo)
        {
            for (int i = 0; i < _str.Length; i++)
            {
                var subStr = Substring(i, replaceFor.Length - 1 + i);
                if (subStr != null && isEquals(subStr.Value, replaceFor.ToCharArray()))
                {
                    var index = 0;
                    for (int j = i; j < replaceTo.Length + i; j++)
                    {
                        _str[j] = replaceTo[index];
                        index++;
                    }
                }
            }
        }

        #region PRIVATE_METHODS       
        private char[] _empty() => new char[_str.Length];

        private void AddToMyString(MyString add)
        {
            if (add.Value.Length == 0) return;

            var strLength = _str.Length;
            Array.Resize(ref _str, _str.Length + add.Value.Length);
            InsertInto(ref _str, add.Value, strLength);
        }

        private void AddToMyString(char[] add)
        {
            if (add.Length == 0) return;

            var strLength = _str.Length;
            Array.Resize(ref _str, _str.Length + add.Length);
            InsertInto(ref _str, add, strLength);
        }

        private void InsertInto(ref char[] array, char[] toAdd, int index)
        {
            if (array.Length - index < toAdd.Length) return;
            foreach (var @char in toAdd)
            {
                array[index] = @char;
                index++;
            }
        }

        private bool isEquals(char[] one, char[] two)
        {
            if (one.Length != two.Length) return false;
            for (int i = 0; i < one.Length; i++)
            {
                if (one[i] != two[i]) return false;
            }

            return true;
        }
        #endregion

        #region Type Conversion Operations

        public static implicit operator string(MyString owner)
        {
            return new string(owner.Value);
        }
        public static explicit operator int(MyString owner)
        {
            return owner.ToInt32();
        }

        public static explicit operator int[] (MyString owner)
        {
            var result = new int[owner.Value.Length];
            for (int i = 0; i < owner.Value.Length; i++)
            {
                result[i] = owner.Value[i];
            }

            return result;
        }
        #endregion
    }
}