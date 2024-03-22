using INFR2100U.CountrySpace;
using INFR2100U.ContinentSpace;
using System.Collections.Generic;
using System;
using System.Linq;

namespace INFR2100U.GraphSpace
{
    public class Graph<T>
    {
        // Dictionary is have a Key as the Node and the List will hold the list of adjacent nodes.
        private Dictionary<T, List<T>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<T, List<T>>();
        }


        #region GRAPH_FUNCTIONS
        /// <summary>
        /// Adds new item to Graph.
        /// </summary>
        /// <param name="node">The key which will be a node in the graph.</param>
        /// <param name="adjacency">The value which will be a list of adjancent nodes.</param>
        public void Add(T node, List<T> adjacency)
        {
            adjacencyList.Add(node, adjacency);
        }

        /// <summary>
        /// Removes item from Graph.
        /// </summary>
        /// <param name="node">Node to remove.</param>
        public void Remove(T node)
        {
            adjacencyList.Remove(node);
        }

        /// <summary>
        /// Addes a Node as a Key to the Dictionary which a List of adjacent nodes can be assigned
        /// </summary>
        /// <param name="node">Node of type T that will be added to the Graph</param>
        public void AddNode(T node)
        {
            if (adjacencyList.ContainsKey(node) == false)
            {
                adjacencyList[node] = new List<T>();
            }
        }

        /// <summary>
        /// Adds the connection between two Nodes. This creates a bi-directional connection.
        /// </summary>
        /// <param name="startNode">The first node that the edge or connection will be drawn from.</param>
        /// <param name="endNode">The node that the edge or connection will end on.</param>
        public void AddEdge(T startNode, T endNode)
        {
            if (adjacencyList.ContainsKey(startNode) == false)
            {
                adjacencyList[startNode].Add(endNode);
            }

            if (adjacencyList.ContainsKey(endNode) == false)
            {
                adjacencyList[endNode].Add(startNode);
            }
        }

        /// <summary>
        /// Get the list of neighboring nodes or the list of connected nodes.
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        public List<T> GetNeighbors(T Node)
        {
            try
            {
                return adjacencyList[Node];
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        #endregion


        #region HELPERS
        /// <summary>
        /// Add support for ForEach statement.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T Node in adjacencyList.Keys)
            {
                yield return Node;
            }
        }

        /// <summary>
        /// Get the value associated with the specified Key.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public List<T> this[T node]
        {
            get
            {
                if (adjacencyList.ContainsKey(node))
                {
                    return adjacencyList[node];
                }

                return new List<T>();
                //throw new KeyNotFoundException();

            }

            set
            {

                if (adjacencyList.ContainsKey(node))
                {
                    adjacencyList[node] = value;
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Get the value associated with the specified Key.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public List<T> this[string name]
        {
            get
            {
                foreach (T node in adjacencyList.Keys)
                {
                    if (node is Country country && country.countryName.ToLower() == name.ToLower() ||
                        node is Continent continent && continent.continentName.ToLower() == name.ToLower())
                    {
                        return adjacencyList[node];
                    }
                }

                return new List<T>();
                //throw new KeyNotFoundException();
            }

            set
            {
                foreach (T node in adjacencyList.Keys)
                {
                    if (node is Country country && country.countryName.ToLower() == name.ToLower() ||
                        node is Continent continent && continent.continentName.ToLower() == name.ToLower())
                    {
                        adjacencyList[node] = value;
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Returns the first node in the Graph
        /// </summary>
        public T FirstNode { get { return adjacencyList.First().Key; } }

        /// <summary>
        /// Finds the first node that is a Country or Continent.
        /// </summary>
        /// <param name="name">Name of the Country or Continent</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public T FindNode(string name)
        {
            foreach (T node in adjacencyList.Keys)
            {
                if (node is Country country && country.countryName.ToLower() == name.ToLower() ||
                    node is Continent continent && continent.continentName.ToLower() == name.ToLower())
                {
                    return node;
                }
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Return true if the node is found.
        /// </summary>
        /// <param name="node">The node of type T which you want to find.</param>
        /// <returns></returns>
        public bool Contains(T node)
        {
            if (adjacencyList.ContainsKey(node))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the Country or Continent is found.
        /// </summary>
        /// <param name="name">The name of the Country or Continent</param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            foreach (T node in adjacencyList.Keys)
            {
                if (node is Country country && country.countryName.ToLower() == name.ToLower() ||
                    node is Continent continent && continent.continentName.ToLower() == name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        // EXISTS ONLY TO CHECK OUT WHAT NEW FUNCTIONS NEEDS TO BE ADDED.
        #region TEST_&_DELETE
        /// <summary>
        /// ONLY FOR TESTING PURPOSES
        /// </summary>
        private void Test()
        {

        }
        #endregion
    }
}
