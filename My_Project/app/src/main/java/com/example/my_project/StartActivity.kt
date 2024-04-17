package com.example.my_project

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Adapter
import android.widget.ArrayAdapter
import android.widget.ListView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import java.lang.Exception
import java.net.Socket
import kotlin.concurrent.thread

class StartActivity:AppCompatActivity() {
    lateinit var _array_command:ArrayList<String>
    var _host:String = ""
    var _user:String = ""
    var _password:String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_start)
        _host = intent.getStringExtra("host").toString()
        _user = intent.getStringExtra("user").toString()
        _password = intent.getStringExtra("password").toString()
        _array_command = ArrayList<String>()
        /*_array_command.add("get_users");
        _array_command.add("get_my_index")*/
        _array_command.add("info_server")
        val list:ListView = findViewById(R.id.list)
        list.adapter = ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,_array_command)
        list.setOnItemClickListener { parent, view, position, id ->
            Toast.makeText(this,_array_command[position],Toast.LENGTH_SHORT).show()
            if(_array_command[position] == "info_server")
            {
                    val starAct = Intent(this, InfoAcivity::class.java);
                    starAct.putExtra("host", _host)
                    starAct.putExtra("user", _user)
                    starAct.putExtra("password", _password)
                    startActivity(starAct)

            }

        }


    }
}