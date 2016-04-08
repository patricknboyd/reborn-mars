using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    public class MapGenerationService : IPassiveService
    {

        /// <summary>
        /// Gets or sets the standard map width.
        /// </summary>
        public int DefaultMapWidth { get; set; }
        /// <summary>
        /// Gets or sets the standard map height;
        /// </summary>
        public int DefaultMapHeight { get; set; }

        private Dictionary<Type, IMapGenerator> MapGenerators { get; set; }

        public MapGenerationService()
        {
            DefaultMapHeight = 20;
            DefaultMapWidth = 79;

            MapGenerators = new Dictionary<Type, IMapGenerator>();
        }

        public void StartService()
        {

        }

        public void EndService()
        {
        }

        public MapState GenerateNewMap(Type generatorType)
        {
            IMapGenerator generator = GetMapGenerator(generatorType);

            IMap map = generator.GenerateMap(DefaultMapWidth, DefaultMapHeight);

            return new MapState(map);
        }
        
        public bool ContainsMapGeneratorOfType(Type generatorType)
        {
            return MapGenerators.Keys.Contains(generatorType);
        }

        public void AddMapGenerator(IMapGenerator generator)
        {
            MapGenerators.Add(generator.GetType(), generator);
        }

        public void AddMapGeneratorType(Type mapGeneratorType)
        {
            try
            {
                IMapGenerator newGenerator = (IMapGenerator)Activator.CreateInstance(mapGeneratorType);

                AddMapGenerator(newGenerator);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException(string.Format(ErrorMessages.TypeDoesNotImplementInterface, mapGeneratorType.GetType().ToString(), "IMapGenerator"), ex);
            }
        }

        public IMapGenerator GetMapGenerator(Type generatorType)
        {
            return MapGenerators[generatorType];
        }

        private IMapGenerator SelectRandomMapGenerator()
        {
            return MapGenerators.Values.ElementAt(Game.Current.RNG.Next(0, MapGenerators.Count));
        }
    }
}
