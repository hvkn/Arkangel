const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name: String,
    user : String,
    enable : Boolean,
    bookmark : Boolean,
    password : Boolean
},
    { collection: "settings" });
module.exports = mongoose.model('modelWeb', schema);