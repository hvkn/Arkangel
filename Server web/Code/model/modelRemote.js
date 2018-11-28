const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    user : String,
    method : String
},
    { collection: "remote" });
module.exports = mongoose.model('modelRemote', schema);