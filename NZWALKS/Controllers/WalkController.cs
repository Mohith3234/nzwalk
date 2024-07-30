using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.Automapper;
using NZWALKS.Data;
using NZWALKS.DTO;
using NZWALKS.Model.Domine;
using NZWALKS.Repository;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly AutomapperProficeClass automapperProficeClass;
        private readonly IWalkREpository walkREpository;
        private readonly DbcontextClass dbcontextClass;
        public WalkController(DbcontextClass dbcontextClass,AutomapperProficeClass automapperProficeClass, IWalkREpository walkREpository)
        {
            this.automapperProficeClass = automapperProficeClass;
            this.walkREpository = walkREpository;
            this.dbcontextClass = dbcontextClass;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkClass addWalkClass)
        {
            //map dto to domine model
            // var walkvalue = automapperProficeClass.Map<Walk>(addWalkClass);
            var walkvalue = new Walk
            {
                Name = addWalkClass.Name,
                Description = addWalkClass.Description,
                LengthInKm = addWalkClass.LengthInKm,
                WalkImageUrl = addWalkClass.WalkImageUrl,
                DeficultyId = addWalkClass.DeficultyId,
                RegionId = addWalkClass.RegionId,

            };
            var walkValue = await walkREpository.CreateAsync(walkvalue);
            //convert domine model to dto
            var walkdomine = new ReturnWalkDtoClass
            {
                Id = walkvalue.Id,
                Description = walkvalue.Description,
                LengthInKm = walkvalue.LengthInKm,
                WalkImageUrl = walkvalue.WalkImageUrl,
                DeficultyId = walkvalue.DeficultyId,
                RegionId = walkvalue.RegionId,
            };

            //convert walk to dto
            // var walkdto=automapperProficeClass.Map<Walk>(walkdomine);
            return Ok(walkdomine);

        }
        [HttpGet]//filtering
        public async Task<IActionResult> GetAll([FromQuery] String? filterOn, [FromQuery] String? filterQuery,[FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {
            var walkdomine =await walkREpository.GetAllAyncc(filterOn,filterQuery,sortBy,isAscending, pageNumber, pageSize);

            //convert walk domine to dto
            var walkdto = new List<ReturnWalkDtoClass>();

            foreach (var walkvalue in walkdomine)
            {
                walkdto.Add(new ReturnWalkDtoClass
                {
                    Id=walkvalue.Id,
                    Name=walkvalue.Name,
                    Description = walkvalue.Description,
                    LengthInKm= walkvalue.LengthInKm,
                    WalkImageUrl = walkvalue.WalkImageUrl,
                    DeficultyId=walkvalue.DeficultyId,
                    RegionId =walkvalue.RegionId,
                    

                });
               
            }
           
            return Ok(walkdomine);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkvalue = await walkREpository.GetByIdAsync(id);
            if(walkvalue == null)
            {
                return BadRequest();
            }
            return Ok(walkvalue);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,CreateWalkDtoClass createWalkDtoClass)
        {
            //var ExitWalkValue = await walkREpository.UpdateAsync(id);
            //convert dto to walk model
            var ExitWalkValue = new Walk();
            {
                ExitWalkValue.Name = createWalkDtoClass.Name;
                ExitWalkValue.Description = createWalkDtoClass.Description;
                ExitWalkValue.LengthInKm = createWalkDtoClass.LengthInKm;
                ExitWalkValue.WalkImageUrl = createWalkDtoClass.WalkImageUrl;
                ExitWalkValue.DeficultyId = createWalkDtoClass.DeficultyId;
                ExitWalkValue.RegionId = createWalkDtoClass.RegionId;
            }
            var ExitWalk = await walkREpository.UpdateAsync(id,ExitWalkValue);

            return Ok(ExitWalk);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var exitvalue=await walkREpository.DeleteAsync(id);
            if(exitvalue == null)
            { 
                return NotFound(); 
            }
            return Ok(exitvalue);

        }

    }
}
