const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    time: String, 
    user : String, 
    content: String, 
    color: String, 
    css: String,
    name: String
},{collection : "web"});
module.exports = mongoose.model('modelFindWebLog', schema);