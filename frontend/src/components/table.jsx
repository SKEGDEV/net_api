import './styles/table.css';
import axios from 'axios';
import {useState, useEffect} from 'react';
import Activate from './helpers/notifications.jsx';
import {Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';


export default function Table(){

  const [trucks, setTrucks] = useState([]);
  const [conductores, setConductores] = useState([]);
  const [ayudantes, setAyudantes] = useState([]);
  //parametros del camion
  const [id_truck, setId_truck] = useState("");
  const [placas, setPlacas] = useState("");
  const [km_por_lt, setKm_por_lt] = useState("");
  const [capacidad_cc, setCapacidad_cc] = useState("");
  const [departamento, setDepartamento] = useState("");
  const [tipo_carga, setTipo_carga] = useState("");
  const [id_conductor, setId_conductor]= useState("");
  const [id_ayudante, setId_ayudante] = useState("");
  //modal
  const [modal_edit, setModal_edit] = useState(false);

  const update_truck = async ()=>{
    const url = "https://localhost:44330/api/truck/"+id_truck;
    const updated_truck = {
      placas: placas,
      km_por_lt: km_por_lt,
      capacidad_cc: capacidad_cc,
      deparatamento: departamento,
      tipo_carga: tipo_carga,
      id_conductor: id_conductor,
      id_ayudante: id_ayudante
    }

    try{
      const msm = await axios.put(url, updated_truck);
      Activate(
	"Todo salio bien",
	"success",
	msm.data[0]['msm']
      );
      window.location.reload();
    }catch(e){
      Activate(
	"Algo ocurrio",
	"danger",
	e.message
      );
    }
  }

  const get_truck_to_update = async (id)=>{
    const url = "https://localhost:44330/api/truck/"+id;
    try{
      const msm = await axios.get(url);
      console.log(msm.data[0]['truck']);
      const truck_data = msm.data[0]['truck'];
     
	setPlacas(truck_data.placas);
	setKm_por_lt(truck_data.km_por_lt);
	setCapacidad_cc(truck_data.capacidad_cc);
	setDepartamento(truck_data.deparatamento);
	setTipo_carga(truck_data.tipo_carga);
	setId_conductor(truck_data.id_conductor);
	setId_ayudante(truck_data.id_ayudante); 
      Activate(
	"Todo es correcto",
	"success",
	msm.data[0]["msm"]
      );
      setId_truck(id);
      setModal_edit(true);
    }catch(e){
      Activate(
	"Algo ocurrio",
	"danger",
	e.message
      );
    }
  }

  const get_employees = async()=>{ 
    const url = "https://localhost:44330/api/chofers_helpers";

    try{
      const data = await axios.get(url);
      setConductores(data.data[0]["chofers"]);
      setAyudantes(data.data[0]["helpers"]);
    }catch(e){
      Activate(
	"Algo ocurrio",
	"danger",
	e.message
      );
    }
  }


  const get_trucks = async()=>{
    const url = "https://localhost:44330/api/truck";
    try{
      const data = await axios.get(url);
      if(data.data[0]['msm'] == "Estos son los datos"){setTrucks(data.data[0]['data']);}
      else{setTrucks([]);}
      console.log(data.data[0]);
    }catch(e){
      alert(e.message);
    }
  }

  const delete_truck = async (id)=>{
    const url = "https://localhost:44330/api/truck/"+id;
    try{
      const msm = await axios.delete(url);
      Activate(
	"Todo esta correcto",
	"success",
	msm.data[0]['msm']
      );
      window.location.reload();
    }catch(e){
      Activate(
	"Algo salio mal",
	"danger",	
	e.message
      );
    }

  }

  useEffect(()=>{
    get_trucks();
    get_employees();
  },[])
  return(
    <div className="reduce-table">
    <table class="table">
      <thead class="table-dark">
        <tr>
          <th>Placa</th>
          <th>Capacidad</th>
          <th>Departamento</th>
          <th>Estado</th>
          <th>KM/LT</th>
          <th>Conductor</th>
          <th>Ayudante</th>
	  <th>Tipo de carga</th>
	  <th></th>
	  <th></th>
        </tr>
      </thead>
      <tbody>
    {trucks.map(data =>(
        <tr>
          <td>{data.placas}</td>
          <td>{data.capacidad_cc+" "}cc</td>
          <td>{data.departamento}</td>
          <td>{data.estado}</td>
          <td>{data.km_por_lt+" "}km/lt</td>
          <td>{data.conductor}</td>
          <td>{data.ayudante}</td>
	  <td>{data.tipo_carga}</td>
	  <td><button onClick={()=>{delete_truck(data.id);}} className="btn btn-danger">Eliminar</button></td>
	  <td><button onClick={()=>{get_truck_to_update(data.id);}} className="btn btn-warning">Editar</button></td>
        </tr>
))}
      </tbody>
    </table>


    <Modal isOpen={modal_edit}>
      <ModalHeader>
        <h1>Agregar un camion</h1>
      </ModalHeader>
      <ModalBody >
        <div class="input"><input class="form-control" type="text" placeholder="Por favor ingrese la placa"
	onChange={(event)=>{setPlacas(event.target.value);}} value={placas}/></div>
        <div class="input"><input class="form-control" type="number" placeholder="por favor ingrese los km/lt"
	  onChange={(event)=>{setKm_por_lt(event.target.value);}} value={km_por_lt}/></div>
        <div class="input"><input class="form-control" type="number" placeholder="por favor ingrese la capacidad en cc"
	onChange={(event)=>{setCapacidad_cc(event.target.value);}} value={capacidad_cc}/></div>
        <div class="input"><input class="form-control" type="text" placeholder="por favor ingrese el departamento"
	onChange={(event)=>{setDepartamento(event.target.value);}} value={departamento}/></div>
	<div class="input">
	  <select className="form-select" onChange={(event)=>{setTipo_carga(event.target.value);}} >
	    <option value="0">Por favor seleccione el tipo de carga</option>
	    <option value="1">Refrigerado</option>
	    <option value="2">No refrigerado</option>
	  </select>
	</div>
	<div class="input">
	  <select className="form-select" onChange={(event)=>{setId_conductor(event.target.value);}} >
	  <option value="0">Por favor seleccione el conductor de este vehiculo</option>
    {conductores.map(conductor=>(
	    <option value={conductor.id}>{conductor.nombre_completo}</option>
    ))}
	  </select>
	</div>
	<div class="input">
	  <select className="form-select" onChange={(event)=>{setId_ayudante(event.target.value);}} id="" name="">
	  <option value="0">Por favor seleccione el ayudante de este vehiculo</option>
    {ayudantes.map(ayudante=>(
	    <option value={ayudante.id}>{ayudante.nombre_completo}</option>
))}
	  </select>
	</div>
      </ModalBody>
      <ModalFooter>
        <button onClick={()=>{setModal_edit(false);}} class="btn btn-danger">Cancelar</button>
        <button onClick={update_truck} class="btn btn-success">Editar</button>
      </ModalFooter>
    </Modal>
    </div>
  );
}
