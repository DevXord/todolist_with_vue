<template>
  <div class="container rounded">
    <div class="row mx-auto">
      <div class="col-12 border fs-20 border-5 border-start-0 border-top-0 border-dark mt-5  bg-white  mb-3 h-100 text-center">
        <div class="mb-4">
         <label for="tb_task" class="form-label">Enter your task</label>
         <input type="text"
           class="form-control w-100" maxlength="300"  v-model="taskText" name="tb_task" id="tb_task"  >
     
       </div>
       <div class="mb-3">
         <label for="tb_taskDate" class="form-label">Date to end</label>
         <input type="datetime-local" 
           class="form-control" v-model="taskDate" name="tb_taskDate" id="tb_taskDate" />

       </div>
       <div class="row mb-2 h-100"> 
         <small id="lb_errorMessage" class="text-danger" v-for="(error, index) in errors" v-bind:key="index" >{{ error }}</small> 
       </div>
        <div class="mb-3">
         <a name="btn_sendTask"
          id="btn_sendTask" 
          @click="addTask()"  
          class="btn btn-dark"  
          role="button">Create task</a>
        </div>
      </div>
    </div>
  </div>
  <Tasks v-for="(task, index) in tasks" 
      v-bind:key="index"
      v-bind:item="task"
      v-bind:index="index"
      v-on:OnClickRemove="removeTask"
      v-on:OnClickCancelEdit="cancelEditTask"
      v-on:OnClickSaveEdit="saveEditTask"
      v-on:OnClickIsTaskDone="taskIsDone" />
       

</template>


<script lang="ts">
 
import { defineComponent } from 'vue';
import Tasks from './Tasks.vue'
import axios from 'axios'
 

    export default defineComponent({

      components: {
          Tasks
        },
        data() {
            return {
                taskText: '',
                taskDate: '',
                errors: [''],
                tasks:  [ 
                    {dataCreate: new Date(),dataEnd: new Date(),iD_Task: 0,isDone: false,taskText: "", isEdit:false}
                ],
                dateBaseConnected: false
            };
        },
        async mounted(){
        
          this.tasks = [];
     
          await this.loadBase();

          this.errors = []
          document.body.style.background = "#bdbdbd"
        },
 
        methods: {
         async taskIsDone(index: number){
           var element = this.tasks[index],obj = {
            iD_Task : element.iD_Task,
            taskText: element.taskText,
            dataEnd: element.dataEnd,
            dataCreate: element.dataCreate,
            isDone: !element.isDone}
            if(this.dateBaseConnected)
            { 
              try {
                await axios.put(import.meta.env.VITE_APP_DATEBASE_API_URL + "Tasks/UpdateTask/",obj);
              }
              catch(error)
              {
                console.log(error);
              }
              await this.reloadTasks();
            }
            else
              this.tasks[index].isDone = true;
          },
          cancelEditTask(task: {index: number , state: boolean}){
            this.tasks[task.index].isEdit = task.state;

          },
          async saveEditTask(task: {index: number , date: Date, text: string}){
              var element =  this.tasks[task.index];
              if(this.dateBaseConnected)
              { 
                try{
                  await axios.put(import.meta.env.VITE_APP_DATEBASE_API_URL + "Tasks/UpdateTask/",
                  {
            

                    
                    dataEnd: task.date.toString() != 'Invalid Date' || task.date.toString() != ''  || task.date.toLocaleString() !=  element.dataEnd.toLocaleString() ?  new Date(task.date) : element.dataEnd,
                    taskText: task.text != '' || task.text != element.taskText ? task.text : element.taskText,
                    iD_Task: element.iD_Task,
                    isDone: element.isDone
                      
                  });
                }
                catch(error){
                  console.log(error);

                }
                await this.reloadTasks();
              }
              else{

                if(task.date.toString() != 'Invalid Date')
                  element.dataEnd = new Date(task.date);
                if(task.text.toString() != '')
                  element.taskText = task.text;
              }

          },
          async reloadTasks(){
            this.tasks = [];
            try{
                  var res = await axios.get(import.meta.env.VITE_APP_DATEBASE_API_URL + "Tasks/GetTasks");
                  for (let i = 0; i < res.data.length; i++) 
                  {
                    const element = res.data[i];
                    this.tasks.push({
                        dataCreate: element.dataCreate,
                        dataEnd: element.dataEnd,
                        iD_Task: element.iD_Task,
                        isDone: element.isDone,
                        taskText: element.taskText,
                        isEdit: false
                    });
                  }
                }
                catch(error){

                  console.log(error)
                }

          },
          async loadBase(){
            try{
                  await axios.post(import.meta.env.VITE_APP_DATEBASE_API_URL + "Datebase/SetDatebaseString", {
                    datebase: import.meta.env.VITE_APP_DATEBASE_TAB_NAME,
                    host: import.meta.env.VITE_APP_DATEBASE_HOST,
                    port: import.meta.env.VITE_APP_DATEBASE_PORT,
                    userName: import.meta.env.VITE_APP_DATEBASE_USER,
                    password: import.meta.env.VITE_APP_DATEBASE_PASSWORD
                  });
                  this.dateBaseConnected = true;
                  await this.reloadTasks();

                }
                catch(error){
                  this.dateBaseConnected = false;
                  console.log(error)
                }
            },

           validateTask(){
            var taskEmpty_isValid = true,taskMinLenght_isValid = true, dateEmpty_isValid = true,
            dateIsFromPast_isValid = true,dateInvalid_isValid=true,mustBeSpaceInTask_isValid = true;
            this.errors = [];
            if(this.taskText == ''){
              this.errors.push("The task field is empty!")
              taskEmpty_isValid= false;
            }
            else if(this.taskText.length < 5){
              this.errors.push("Not enough characters in the task field!")
              taskMinLenght_isValid= false;
            }

            if(this.taskText.length >= 100)
            {
              if(this.taskText.includes(' ')){
                  var tabeCheckSpaceLenght = this.taskText.split(' ');
                  if(tabeCheckSpaceLenght.length >= 10){

                    this.errors.push("Spaces are missing in the task body!")
                    mustBeSpaceInTask_isValid = false;
                  } 
              }
              else
              {
                this.errors.push("Spaces are missing in the task body!")
                mustBeSpaceInTask_isValid = false;

              }
            }
            if(this.taskDate == ''){
              this.errors.push("The date field is empty!")
              dateEmpty_isValid= false;
            }
            else if(new Date(this.taskDate).toString() == 'Invalid Date'){
              this.errors.push("The date is invalid")
              dateInvalid_isValid = false;

            }
            else if(new Date(this.taskDate) <  new Date())
            {
              this.errors.push("The date can't be from the past")
              dateIsFromPast_isValid = false;
            }
            return (
              taskEmpty_isValid && 
              taskMinLenght_isValid && 
              dateEmpty_isValid && 
              dateIsFromPast_isValid &&
              dateInvalid_isValid &&
              mustBeSpaceInTask_isValid
             )
          },


          async addTask(){
            if(this.validateTask())
            { 
              var obj = {
                  
                DataCreate: new Date(),
                DataEnd: new Date( this.taskDate),
                IsDone: false,
                TaskText: this.taskText,
              }

              if(this.dateBaseConnected)
              {                 
                try {
                  await axios.put(import.meta.env.VITE_APP_DATEBASE_API_URL + "Tasks/CreateTask/",
                    obj);

                }
                catch(error)
                {
                  console.log(error);
                } 
                await this.reloadTasks();
              }
              else{

              this.tasks.push({
                  dataCreate: obj.DataCreate,
                  dataEnd: obj.DataEnd,
                  iD_Task: 0,
                  isDone: obj.IsDone,
                  taskText: obj.TaskText,
                  isEdit: false
                });
                
              }
              this.taskDate = '';
              this.taskText='';
            }
          },
          async removeTask(index: number){
            if(this.dateBaseConnected)
            {
              try {
                await axios.delete(import.meta.env.VITE_APP_DATEBASE_API_URL + "Tasks/DeleteTask/"+  this.tasks[index].iD_Task);

              }
              catch(error)
              {
                console.log(error);
              }
              await this.reloadTasks();
            }
            else
              this.tasks.splice(index, 1)
          },
        }, 
    });
</script>