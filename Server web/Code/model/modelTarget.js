const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name : String, 
    user : String,
    enableAll: Boolean,
    enableOnly: Boolean
},
    {collection : "settings"});
module.exports = mongoose.model('modelTarget', schema);