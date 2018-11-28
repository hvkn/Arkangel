const mongoose = require('mongoose');
var schema = new mongoose.Schema({ 
    name: String, 
    user : String,
    application : Array,
    title : Array }, 
    { collection: "settings" });
module.exports = mongoose.model('modelKeywordTarget', schema);