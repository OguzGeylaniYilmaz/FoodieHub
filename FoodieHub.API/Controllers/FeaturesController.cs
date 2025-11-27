using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.FeatureDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public FeaturesController(IMapper mapper, ApiContext apiContext)
        {
            _mapper = mapper;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult GetFeatures()
        {
            var features = _apiContext.Features.ToList();
            return Ok(_mapper.Map<List<ResultFeatureDto>>(features));
        }

        [HttpGet("{id}")]
        public IActionResult GetFeature(int id)
        {
            var feature = _apiContext.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetByIdFeatureDto>(feature));
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            _apiContext.Features.Add(feature);
            _apiContext.SaveChanges();
            return Ok("Feature created successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _apiContext.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }
            _apiContext.Features.Remove(feature);
            _apiContext.SaveChanges();
            return Ok("Feature deleted successfully.");
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto featureDto)
        {
            var existingFeature = _apiContext.Features.FirstOrDefault(f => f.FeatureID == featureDto.FeatureID);
            if (existingFeature == null)
            {
                return NotFound();
            }
            _mapper.Map(featureDto, existingFeature);
            _apiContext.SaveChanges();
            return Ok("Feature updated successfully.");
        }
    }
}
