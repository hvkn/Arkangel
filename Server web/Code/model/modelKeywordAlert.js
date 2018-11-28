const mongoose = require('mongoose');
var schema = new mongoose.Schema({ 
    name: String, 
    user : String,
    list : Array }, 
    { collection: "settings" });
module.exports = mongoose.model('modelKeywordAlert', schema);