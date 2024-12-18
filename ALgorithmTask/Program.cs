namespace ALgorithmTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Kruskal = new KruskalMST();
            Kruskal.printKruskal();
            //=============================
            Console.WriteLine("================================");
            var heap = new HeapSort();
            heap.PrintHeapSort();

        }
        class KruskalMST
        {
            class Edge
            {
                public int Source { get; set; }
                public int Destination { get; set; }
                public int Weight { get; set; }

                public Edge(int source, int destination, int weight)
                {
                    Source = source;
                    Destination = destination;
                    Weight = weight;
                }
            }

            class DisjointSet
            {
                private int[] parent;
                private int[] rank;

                public DisjointSet(int size)
                {
                    parent = new int[size];
                    rank = new int[size];

                    for (int i = 0; i < size; i++)
                    {
                        parent[i] = i;
                        rank[i] = 0;
                    }
                }

                public int Find(int x)
                {
                    if (parent[x] != x)
                    {
                        parent[x] = Find(parent[x]);  // Path compression
                    }
                    return parent[x];
                }

                public void Union(int x, int y)
                {
                    int rootX = Find(x);
                    int rootY = Find(y);

                    if (rootX != rootY)
                    {
                        if (rank[rootX] > rank[rootY])
                            parent[rootY] = rootX;
                        else if (rank[rootX] < rank[rootY])
                            parent[rootX] = rootY;
                        else
                        {
                            parent[rootY] = rootX;
                            rank[rootX]++;
                        }
                    }
                }
            }

            static List<Edge> Kruskal(int vertices, List<Edge> edges)
            {
                List<Edge> mst = new List<Edge>();
                DisjointSet ds = new DisjointSet(vertices);

                // Sort the edges by weight
                edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

                foreach (var edge in edges)
                {
                    int rootSource = ds.Find(edge.Source);
                    int rootDestination = ds.Find(edge.Destination);

                    if (rootSource != rootDestination)
                    {
                        mst.Add(edge);
                        ds.Union(rootSource, rootDestination);
                    }

                    if (mst.Count == vertices - 1)
                        break;
                }

                return mst;
            }
            public void printKruskal()
            {
                int vertices = 4;
                List<Edge> edges = new List<Edge>
                    {
                        new Edge(0, 1, 10),
                        new Edge(0, 2, 6),
                        new Edge(0, 3, 5),
                        new Edge(1, 3, 15),
                        new Edge(2, 3, 4)
                    };

                List<Edge> mst = Kruskal(vertices, edges);

                Console.WriteLine("Edges in MST:");
                foreach (var edge in mst)
                {
                    Console.WriteLine($"{edge.Source} - {edge.Destination} : {edge.Weight}");
                }
            }
        }
        class HeapSort
        {
            public static void Sort(int[] array)
            {
                int n = array.Length;

                // Step 1: Build Max-Heap
                for (int i = n / 2 - 1; i >= 0; i--)
                    Heapify(array, n, i);

                // Step 2: Extract elements from the heap one by one
                for (int i = n - 1; i > 0; i--)
                {
                    // Swap root (max element) with the last element
                    (array[0], array[i]) = (array[i], array[0]);

                    // Restore the heap property
                    Heapify(array, i, 0);
                }
            }

            private static void Heapify(int[] array, int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1; // Left child index
                int right = 2 * i + 2; // Right child index

                if (left < n && array[left] > array[largest])
                    largest = left;

                if (right < n && array[right] > array[largest])
                    largest = right;

                if (largest != i)
                {
                    (array[i], array[largest]) = (array[largest], array[i]);
                    Heapify(array, n, largest); // Recursively heapify the affected subtree
                }
            }

            public void PrintHeapSort()
            {
                int[] numbers = { 12, 11, 13, 5, 6, 7 };

                Console.WriteLine("Original array:");
                Console.WriteLine(string.Join(" ", numbers));

                Sort(numbers);

                Console.WriteLine("Sorted array:");
                Console.WriteLine(string.Join(" ", numbers));
            }
        }
    }
}
