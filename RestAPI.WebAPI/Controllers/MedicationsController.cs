using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : Controller
    {
        // GET: api/<MedicationsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Medication>> GetMedications()
        {
            return Ok(Medications.GetAllMedications());
        }

        // DELETE: api/<MedicationsController>/{medication_code}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> DeleteMedication([FromBody] int medication_code)
        {
            try
            {
                string result = Medications.Delete_Medication(medication_code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/<MedicationsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> PostMedication([FromBody] Medication medication)
        {
            if (medication.Quantity < 0)
            {
                return BadRequest("Quantity can´t be a negative value!");
            }
            if (Medications.Verify_Medication_by_Name(medication.Name))
            {
                return BadRequest("Medication with same name allready exists in the database");
            }

            try
            {
                string result = Medications.Add_Medication(medication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
