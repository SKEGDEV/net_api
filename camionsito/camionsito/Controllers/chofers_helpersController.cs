using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using camionsito.Models;
using camionsito.Models.empleado_model;

namespace camionsito.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class chofers_helpersController : ApiController
    {
        public IHttpActionResult GetChoffers()
        {
            try
            {
                List<get_employee> get_chofers;
                List<get_employee> get_helpers;
                using(camionsitoEntities db = new camionsitoEntities())
                {
                    get_chofers = (from d in db.conductors
                                     select new get_employee
                                     {
                                         id = d.id,
                                         nombre_completo = d.nombre_completo,
                                         DPI = (int)d.DPI,
                                         NIT = (int)d.NIT,
                                         edad = (int)d.edad
                                     }).ToList();
                    get_helpers = (from d in db.ayudantes
                                   select new get_employee
                                   {
                                       id = d.id,
                                       nombre_completo = d.nombre_completo,
                                       DPI = (int)d.DPI,
                                       NIT = (int)d.NIT,
                                       edad = (int)d.edad
                                   }).ToList();
                }
                List<json_employee> response_employees = new List<json_employee>
                {
                    new json_employee
                    {
                        msm = "Estos son los datos",
                        chofers = get_chofers,
                        helpers = get_helpers
                    }
                };
                return Ok(response_employees);


            }catch (Exception ex)
            {
                List<json_error> errors = new List<json_error> {
                    new json_error
                    {
                        msm="Ha ocurrido un error inesperado",
                        error = ex.Message
                    }
                };
                return Ok(errors);
            }
        }
        [ResponseType(typeof(add_employee))]
        public IHttpActionResult PostEmployees(int type, add_employee new_employee)
        {
            try
            {
                List<json_success> added_success = new List<json_success>
                {
                    new json_success
                    {
                        msm="Se ha agregado correctamente"
                    }
                };

                if(type == 1)
                {
                    var chofer = new conductor();
                    chofer.nombre_completo = new_employee.nombre_completo;
                    chofer.DPI = new_employee.DPI;
                    chofer.NIT = new_employee.NIT;
                    chofer.edad = new_employee.edad;
                    using(camionsitoEntities db = new camionsitoEntities())
                    {
                        db.conductors.Add(chofer);
                        db.SaveChanges();
                    }
                    return Ok(added_success);
                }
                else
                {
                    var helper = new ayudante();
                    helper.nombre_completo = new_employee.nombre_completo;
                    helper.DPI = new_employee.DPI;
                    helper.NIT = new_employee.NIT;
                    helper.edad = new_employee.edad;

                    using(camionsitoEntities db = new camionsitoEntities())
                    {
                        db.ayudantes.Add(helper);
                        db.SaveChanges();
                    }
                    return Ok(added_success);
                }

            }catch (Exception ex)
            {
                List<json_error> errors = new List<json_error>
                {
                    new json_error
                    {
                        msm = "Un error ha ocurrido",
                        error =ex.Message
                    }
                };
                return Ok(errors);
            }
        }

        
    }
}
