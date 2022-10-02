# AlkemyWallet


## **Especificación de la Arquitectura**

### **Capa Controller**
Será el punto de entrada a la API. En los controladores deberíamos definir la menor cantidad de lógica posible y utilizarlos como un pasamanos con la capa de servicios.

### **Capa DataAccess**
Es donde definiremos el DbContext y crearemos los seeds correspondientes parapopular nuestras entidades.

### **Capa Entities**
En este nivel de la arquitectura definiremos todas las entidades de la base de datos.

### **Capa Repositories**
En esta capa definiremos las clases correspondientes para realizar el repositorio genérico y la unidad de trabajo

### **Capa Controller**
Será el punto de entrada a la API. En los controladores deberíamos definir la menor cantidad de lógica posible y utilizarlos como un pasamanos con la capa de servicios.

### **Capa Core**
Es nuestra capa principal, en ella encontramos varios subniveles

*	Helper: Definiremos lógica que pueda ser de utilidad para todo el proyecto. Por ejemplo, algún método para encriptar/desencriptar contraseñas
*	Interfaces: Se definirán las interfases que utilizaremos en los servicios.
*	Mapper: En esta carpeta irán las clases de mapeo para vincular entidad-dto o dto-entidad
*	Models: se definirán los modelos que necesitemos para el desarrollo. Dentro de esta carpeta encontramos DTO, para definirlos ahí dentro.
*	Services: Se incluirán todos los servicios solicitados por el proyecto.

## **Especificación de GIT**

* Se deberán crear las ramas a partir de DEV. La nomenclatura para el nombre de las ramas será la sigueinte: Feature/Us-xx (donde xx corresponde con el número de historia)
* El título del pull request debe contener el título de la historia tomada.
* Los commits deben llevar descripciones.
* El pull request solo debe contener cambios relacionados con la historia tomada.
* Se deben agregar capturas de pantalla como evidencia en la descripción de los puul request.

