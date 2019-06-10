using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    class MapMarkerService : IMapMarkerService
    {
        private readonly IMarkersRepository _markersRepository;

        public MapMarkerService(IMarkersRepository markersRepository)
        {
            _markersRepository = markersRepository;
        }

        public void InsertMarker(MapMarkerEntity marker)
        {
            _markersRepository.InsertMarker(marker);
        }

        public void UpdateMarkers(List<MapMarkerEntity> list)
        {
            int taskId = list[0].TaskId;
            _markersRepository.DeleteMarkers(taskId);

            _markersRepository.InsertMarkers(list);
        }

        public List<MapMarkerEntity> GetMarkerList(int taskId)
        {
            return _markersRepository.GetMarkersList(taskId);
        }

        public void DeleteMarkers(int taskId)
        {
            _markersRepository.DeleteMarkers(taskId);
        }
    }
}
