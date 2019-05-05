using UnityEngine;

// [REFACTOR] Break into seperate classes based on purpose
public class CustomMath
{
    // * Move to VectorMath
    public Vector3 Span2Nodes(Transform spanner, Transform t1, Transform t2)
    {
        spanner.position = Midpoint(t1.position, t2.position);

        spanner.LookAt(t2, Vector3.up);
        spanner.Rotate(0f, -90f, 0f);

        return spanner.rotation.eulerAngles;
    }

    public Vector3 Midpoint(Vector3 point1, Vector3 point2)
    {
        return new Vector3(Midpoint(point1.x, point2.x), Midpoint(point1.y, point2.y), Midpoint(point1.z, point2.z));
    }

    public float Midpoint(float point1, float point2)
    {
        return (point1 - point2) / 2 + point2;
    }

    public float Vector2Degrees(Vector2 point)
    {
        float degrees = Vector2.Angle(Vector2.up, point);

        if (point.x < 0)
            degrees += 2 * (180 - degrees);

        return degrees;
    }


    // * Move to ShuffleArray
    public bool[] IntToBinary(int number)
    {
        int binaryLength = 1;
        int n = number;

        while (n > 1)
        {
            n = Mathf.FloorToInt(n/2);
            binaryLength++;
        }

        bool[] binary = new bool[binaryLength];
        n = number;

        for (int i = 0; i < binaryLength; i++)
        {
            if (n % 2 == 0)
                binary[i] = false;
            else
                binary[i] = true;

            n = Mathf.FloorToInt(n/2);
        }

        return binary;
    }

    public T[] Shuffle<T>(T[] items)
    {
        T[] shuffledItems = new T[items.Length];
        int[] shuffledPositions = RandomIntArray(items.Length);

        for (int i = 0; i < items.Length; i++)
            shuffledItems[i] = items[shuffledPositions[i]];

        return shuffledItems;
    }

    public int[] RandomIntArray(int length)
    {
        int[] numbers = new int[length];

        for(int i = 0; i < numbers.Length; i++)
            numbers[i] = -1;

        int r = -1;
        int failCount;
        bool fail = false;

        for (int i = 0; i < length; i++)
        {
            failCount = 0;

            while(failCount < 100)
            {
                fail = false;
                r = Random.Range(0, length);

                for (int j = 0; j < length; j++)
                {
                    if (numbers[j] == r)
                    {
                        fail = true;
                        failCount++;
                        break;
                    }
                }

                if(!fail)
                    break;
            }
            
            if (fail || r == -1)
                Debug.LogError("Failed to place item " + i + " into array");
            else
                numbers[i] = r;
        }

        return numbers;
    }
}
