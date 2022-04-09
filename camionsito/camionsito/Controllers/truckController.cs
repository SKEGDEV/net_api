using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using camionsito.Models.camion_model;
using camionsito.Models;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace camionsito.Controllers
{
    [EnableCors(origins: "*", headers:"*", methods:"*")]
    public class truckController : ApiController
    {
       
        public IHttpActionResult GetTruck()
        {
            try { 
                List<truck_get> get_trucks;
                using(camionsitoEntities db = new camionsitoEntities())
                {
                 get_trucks = (from d in db.get_trucks()
                                  select new truck_get
                                  {
                                  id = d.id,
                                  placas = d.placas,
                                  capacidad_cc = (float)d.capacidad_cc,
                                  departamento = d.departamento,
                                  estado = d.estado,
                                  km_por_lt = (int)d.km_por_lt,
                                  conductor = d.conductor,
                                  ayudante = d.ayudante,
                                  tipo_carga = d.tipo_carga

                                  }).ToList();
                }
                List<json_truck> trucks = new List<json_truck>
                {
                    new json_truck
                    {
                        msm = "Estos son los datos",
                        data = get_trucks
                    }
                };
                return Ok(trucks);
            }catch (Exception ex)
            {
                List<json_error> errors = new List<json_error>
                {
                    new json_error
                    {
                        msm="Perdon un error ha ocurrido",
                        error = ex.Message
                    }
                };
                return Ok(errors);
            }
        }
        public IHttpActionResult GetTruck(int id)
        {
            try
            {
                truck_get_update model = new truck_get_update();
                using(camionsitoEntities db = new camionsitoEntities())
                {
                    var find_truck = db.camions.Find(id);
                    model.placas = find_truck.placas;
                    model.km_por_lt = (int)find_truck.km_por_lt;
                    model.capacidad_cc = (float)find_truck.capacidad_cc;
                    model.deparatamento = find_truck.departamento;
                    model.tipo_carga = (int)find_truck.tipo_carga;
                    model.id_conductor = (int)find_truck.id_conductor;
                    model.id_ayudante = (int)find_truck.id_ayudante;    

                }
                List<json_truck_update> response_truck = new List<json_truck_update>
                {
                    new json_truck_update
                    {
                        msm = "Estos son los datos",
                        truck = model
                    }
                };
                return Ok(response_truck);
            }
            catch (Exception ex)
            {
                List<json_error> errors = new List<json_error>
                {
                    new json_error
                    {
                        msm = "Un error inesperado ha ocurrido",
                        error=ex.Message
                    }
                };
                return Ok(errors);
            }
        }

        [ResponseType(typeof(camion))]
        public IHttpActionResult PostTruck(camion new_truck)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<json_success> not_valid_format = new List<json_success>
                    {
                        new json_success
                        {
                            msm="Perdon pero el formato no es valido"
                        }
                    };
                    return Ok(not_valid_format);
                }

                List<json_success> is_added = new List<json_success>
                {
                    new json_success
                    {
                        msm="Todo esta correcto se ha insertado con exito"
                    }
                };

                using (camionsitoEntities db = new camionsitoEntities()) { 

                    db.camions.Add(new_truck);
                    db.SaveChanges();

                }
                return Ok(is_added);
            }
            catch (Exception ex)
            {
                List<json_error> Errors = new List<json_error>
                {
                    new json_error
                    {
                        msm = "Un error inesperado ha ocurrido",
                        error = ex.Message
                    }
                };
                return Ok(Errors);
            }
        }

        [ResponseType(typeof(camion))]
        public IHttpActionResult DeleteTruck(int id)
        {
            try { 
            camion truck;
            using (camionsitoEntities db = new camionsitoEntities())
            {

                truck = db.camions.Find(id);
                if(truck == null)
                    {
                        List<json_success> not_exist = new List<json_success>
                        {
                            new json_success{
                                msm = "Perdon pero este camion no existe"
                            }
                        };
                        return Ok(not_exist);
                    }
                db.camions.Remove(truck);
                db.SaveChanges();
                    List<json_success> successful = new List<json_success>
                {
                    new json_success
                    {
                        msm = "Todo es correcto se ha eliminado con exito"
                    }
                };
                    return Ok(successful);
            }
            }catch (Exception ex)
            {
                List<json_error> Errors = new List<json_error>
                {
                    new json_error
                    {
                        msm="ha ocurrido un error inesperado",
                        error = ex.Message
                    }
                };
                return Ok(Errors);
            }
        }
        [ResponseType(typeof(truck_get_update))]
        public IHttpActionResult PutTruck(int id, truck_get_update new_truck)
        {
            try
            {
                using(camionsitoEntities db = new camionsitoEntities())
                {
                    var truck_result = db.camions.Find(id);
                    truck_result.placas = new_truck.placas;
                    truck_result.km_por_lt = new_truck.km_por_lt;
                    truck_result.capacidad_cc = new_truck.capacidad_cc;
                    truck_result.departamento = new_truck.deparatamento;
                    truck_result.tipo_carga = new_truck.tipo_carga;
                    truck_result.id_conductor = new_truck.id_conductor;
                    truck_result.id_ayudante = new_truck.id_ayudante;
                    db.Entry(truck_result).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                List<json_success> success = new List<json_success>
                {
                    new json_success
                    {
                        msm = "Se ha actualizado con exito"
                    }
                };
                return Ok(success);
            }
            catch (Exception ex)
            {
                List<json_error> errors = new List<json_error>
                {
                    new json_error
                    {
                        msm = "Un error ha ocurrido",
                        error=ex.Message
                    }
                };
                return Ok(errors);
            }
        }

    }
}
