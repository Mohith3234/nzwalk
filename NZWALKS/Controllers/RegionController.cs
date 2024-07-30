using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWALKS.Automapper;
using NZWALKS.Data;
using NZWALKS.DTO;
using NZWALKS.Model.Domine;
using NZWALKS.Repository;
//using Microsoft.AspNetCore.Routing.RouteOptions.ContrainType;


namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly DbcontextClass dbcontext;
        private readonly RepositoryPattern repositoryPattern;
        private readonly AutomapperProficeClass automapperProficeClass;
        public RegionController(DbcontextClass dbcontext,RepositoryPattern repositoryPattern,AutomapperProficeClass automapper) { 
            this.dbcontext = dbcontext;
            this.repositoryPattern = repositoryPattern;
            this.automapperProficeClass = automapper;

        }

        [HttpGet]
        //Get All Rows Regions
        public async Task<IActionResult> GetAll()
        {
            //get data from data base-domine models
           // var RegionDomine=await dbcontext.regions.ToListAsync();
           var RegionDomine=await repositoryPattern.GetAllAsync();
            //Map Domine models to Dtos
            var RegionDtos=new List<RegionDto>();

            foreach (var region in RegionDomine)
            {
                RegionDtos.Add(new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionUrl = region.RegionUrl,
                });
            }
            //map domine model to dtos
           //var RegionDtos=automapperProficeClass<List<RegionDto>>(RegionDomine);   
            
            //Return Dtos
            return Ok(RegionDtos);
        }

        [HttpGet]
        //Get Single Row Based On Id
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //Get RegionDomine Model from Database
            //var region= await dbcontext.regions.FindAsync(id);
            var region= await repositoryPattern.GetByAsync(id);
            //var region=dbcontext.regions.FirstOrDefault(x=>x.Id==id);
            if(region == null)
            {
                return BadRequest();
            }
            //convert RegionDomine into Dto
             var RegionDto = new RegionDto
             {
                 Id = region.Id,
                 Name = region.Name,
                 Code = region.Code,
                 RegionUrl = region.RegionUrl,
             };
             //Return data to the client
           // var RegionDto = automapperProficeClass<RegionDto>(region);

            return Ok(RegionDto);
        }
        //create region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDtoClass addregiondtoclass)
        {
            //Map or Convert to Dto to Domine model
            var regiondominemodel = new Region
            {

                Name = addregiondtoclass.Name,
                Code = addregiondtoclass.Code,
                RegionUrl = addregiondtoclass.RegionUrl
            };
            //await dbcontext.regions.AddAsync(regiondominemodel);
            //await dbcontext.SaveChangesAsync();
            regiondominemodel=await repositoryPattern.CreateAsync(regiondominemodel);
           
            //convert domine model to dto
            var regiondto = new RegionDto
            {
                Id = regiondominemodel.Id,
                Name = regiondominemodel.Name,
                Code = regiondominemodel.Code,
                RegionUrl = regiondominemodel.RegionUrl,
            };
            //return CreatedAtAction(nameof(GetById),new {id=regiondto.Id},regiondto);
            return Ok(regiondto);

        }

        //update region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]UpdateClass updateClass)
        {
            //var regionmodel=dbcontext.regions.Find(id);
            //var regionmodel= await dbcontext.regions.FirstOrDefaultAsync(r => r.Id == id);
            //map dto to region
            var regionnew = new Region 
            {
                Name = updateClass.Name,
                Code = updateClass.Code,
                RegionUrl = updateClass.RegionUrl,
            };
            var regionmodel = await repositoryPattern.UpdateAsync(id,regionnew);
            if(regionmodel == null)
            {
                return NotFound();
            }
            
            //convert region model to dto
            var regiondto = new RegionDto
            {
                Id = regionmodel.Id,
                Name = regionmodel.Name,
                Code = regionmodel.Code,
                RegionUrl = regionmodel.RegionUrl,
            };
            return Ok(regiondto);
        }
        //delete region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionmodel= await dbcontext.regions.FirstOrDefaultAsync(x=>x.Id==id);
            if( regionmodel == null)
            {
                return NotFound();
            }
           // dbcontext.regions.Remove(regionmodel);
            //dbcontext.SaveChanges();
            //convert model to dto
            var regiondto = new RegionDto
            {
                Id = regionmodel.Id,
                Name = regionmodel.Name,
                Code = regionmodel.Code,
                RegionUrl = regionmodel.RegionUrl,
            };
            return Ok(regiondto);
        }

    }
}
