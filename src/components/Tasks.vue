<template>
    <div class="container">
        <div
            class="row mb-1 pb-2 pt-2"
            v-bind:class="{
                TaskDone: item.isDone,
                TaskEdit: item.isEdit,
                item: item.isEdit == false  && item.isDone == false,
            }" >
            <div class="col-12 text-center "> 
                <span v-if="item.isEdit == false" class="h5 text-justify ">{{item.taskText}}</span>
                <div v-else class="mb-4">
                <label for="tb_task" class="form-label">Edit your task</label>
                <input type="text"
                        class="form-control w-100 col-md-5"
                        maxlength="300"
                        v-model="taskEditText" 
                        
                        id="tb_taskEdit"  >
                </div>   

            </div>
            <div class="row mt-3  align-bottom text-sm-end">  
                <span v-if="item.isEdit == false" > {{item.dataEnd.toLocaleString().replace('T', ' ')}}</span> 
                <div v-else class="col-12 text-center mb-3">
                    <label for="tb_taskDate" class="form-label">Edit task date</label>
                    <input type="datetime-local"
                        class="form-control col-md-5"   v-model="taskEditDate" name="tb_taskDate" id="tb_taskDate" />
                </div>
                <div class="row text-center" v-if="item.isEdit==true">  
                    <span class="  text-danger" v-for="(error, index) in errorsEdit" v-bind:key="index" >{{ error }}</span> 
                </div>
                
                <div class="mt-4 d-md-flex justify-md-content-between"> 
                    <a v-if="item.isEdit==false && item.isDone == false" @click="isTaskDone(index)" class="col-6 col-sm-2 col-md-3 btn btn-success" role="button">Done</a>
                    <a v-if="item.isEdit==false && item.isDone == false" @click="item.isEdit = true" class="col-6 col-sm-2 col-md-3  btn btn-primary "  role="button">Edit</a>
                    <a v-if="item.isEdit==false" @click="removeTask(index)" id="btn_deleteTask" class="col-6 col-sm-2 col-md-3 btn btn-danger " role="button">Delete</a>
                
                    <a v-if="item.isEdit==true " @click="cancelEdit(index)"      class="col-sm-4 col-md-3   btn btn-danger " role="button">Cancel</a>
                    <a v-if="item.isEdit==true " @click="saveEditChanges(index)" class="col-sm-4 col-md-3   btn btn-primary "  role="button">Save change</a>
                
                </div>
            </div>
        </div>
    </div>
</template>
  

<script lang="ts">
 
import { defineComponent } from 'vue';
 

    export default defineComponent({
        props: ['item', 'index' ],
        data() {
            return {
                taskEditText: '',
                taskEditDate: '',
                errorsEdit: ['']
            };
        },
        mounted(){
           this.taskEditText= '',
           this.taskEditDate= '',
           this.errorsEdit= ['']
        },
        
        methods: {
          removeTask(id:number){
            this.$emit('OnClickRemove',id)
          },
          cancelEdit(index: number){
            this.taskEditDate = '';
            this.taskEditText='';
            this.$emit('OnClickCancelEdit',{index: index , state: false})
          },
          saveEditChanges(index: number){
            if( this.validateEditTask()){
                var date = new Date(this.taskEditDate)
                var text =  this.taskEditText;
                this.$emit('OnClickSaveEdit',{index: index , date: date, text: text})
                this.cancelEdit(index);
            }
          },

          validateEditTask(){
            var taskEmpty_isValid = true,taskMinLenght_isValid = true, dateEmpty_isValid = true,
            dateIsFromPast_isValid = true,dateInvalid_isValid=true, mustBeSpaceInTask_isValid=true;
            this.errorsEdit = [];
            if(this.taskEditText == '' ){
              taskEmpty_isValid = false;
            }

            if(this.taskEditDate == ''){
              dateEmpty_isValid= false;
            }
  
            if(!taskEmpty_isValid && !dateIsFromPast_isValid) {
            this.errorsEdit.push("one of the fields must be completed!")
   
            }
            if(this.taskEditText != '' && this.taskEditText.length < 5){
              this.errorsEdit.push("Not enough characters in the task field!")
              taskMinLenght_isValid= false;
            }

            if(this.taskEditText != '' && this.taskEditText.length >= 100)
            {
              if(this.taskEditText.includes(' ')){
                  var tabeCheckSpaceLenght = this.taskEditText.split(' ');
                  if(tabeCheckSpaceLenght.length  >= 10){

                    this.errorsEdit.push("Spaces are missing in the task body!")
                    mustBeSpaceInTask_isValid = false;
                  } 
              }
              else
              {
                this.errorsEdit.push("Spaces are missing in the task body!")
                mustBeSpaceInTask_isValid = false;

              }
            }

 
            if(this.taskEditDate != '' && new Date(this.taskEditDate).toString() == 'Invalid Date'){
              this.errorsEdit.push("The date is invalid!")
              dateInvalid_isValid = false;

            }
            else if(this.taskEditDate != '' && new Date(this.taskEditDate) <  new Date())
            {
              this.errorsEdit.push("The date can't be from the past!")
              dateIsFromPast_isValid = false;
            }

 


            return (
              (taskEmpty_isValid || !dateIsFromPast_isValid) &&
              taskMinLenght_isValid && 
              dateIsFromPast_isValid &&
              dateInvalid_isValid &&
              mustBeSpaceInTask_isValid)
          },

          isTaskDone(index: number){
            this.$emit('OnClickIsTaskDone', index )
          },

        

        },
        
    });
</script>


<style> 

.TaskDone{
    background: green;
    border: 10px solid black;
    color: aliceblue;
    border-left: 0;
    border-top: 0;

}
.TaskEdit{
    background: rgb(243, 171, 3);
    
    border: 10px solid black;
    border-left: 0;
    border-top: 0;
}
.item {

   background: white;
    border: 10px solid black;
    border-left: 0;
    border-top: 0;


}

</style>