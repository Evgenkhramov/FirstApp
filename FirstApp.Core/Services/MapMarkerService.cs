using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    class MapMarkerService : IMapMarkerService
    {
        private readonly IMarkersRepositoryService _markersRepositoryService;

        public MapMarkerService(IMarkersRepositoryService markersRepositoryService)
        {
            _markersRepositoryService = markersRepositoryService;
        }

        public void InsertMarker(MapMarkerEntity marker)
        {
            _markersRepositoryService.InsertMarker(marker);
        }

        public void UpdateMarkers(List<MapMarkerEntity> list)
        {
            int taskId = list[0].TaskId;
            _markersRepositoryService.DeleteMarkers(taskId);

            _markersRepositoryService.InsertMarkers(list);
        }

        public List<MapMarkerEntity> GetMarkerList(int taskId)
        {
            return _markersRepositoryService.GetMarkersList(taskId);
        }

        public void DeleteMarkers(int taskId)
        {
            _markersRepositoryService.DeleteMarkers(taskId);
        }
    }
}
