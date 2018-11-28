const mongoose = require('mongoose');
var schema = new mongoose.Schema({date:String, user : String,time: Array},{collection : "webcam"});
module.exports = mongoose.model('modelFindWebcam', schema);