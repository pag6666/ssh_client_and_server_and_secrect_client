package com.example.my_project

import android.annotation.SuppressLint
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.ListAdapter
import android.widget.ListView
import androidx.appcompat.app.AppCompatActivity
import java.io.InputStream
import java.io.OutputStream
import java.net.Socket

class InfoAcivity: AppCompatActivity() {
    private lateinit var list:ListView;
    private  lateinit var array_data:ArrayList<String>
    @SuppressLint("MissingInflatedId")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_info);
        list = findViewById(R.id.list_info)
        array_data = ArrayList()


    }
    private fun Write(write:OutputStream ,text:String):Boolean
    {
        var check_connected = false
        try
        {
            var buffer:ByteArray = text.toByteArray(Charsets.UTF_8)
            check_connected = true
            //code next
            write.write(buffer, 0, buffer.size)
            write.flush()
        }
        catch (e:Exception)
        {
            check_connected = false
            Log.d("error_connect_write",e.message.toString())
        }
        return check_connected
    }

    private fun Read(read:InputStream, text:StringBuilder):Boolean
    {
        var buffer = ByteArray(1024)
        var check_connected = false
        try
        {
            check_connected = true
            //code next
            val size = read.read(buffer,0,buffer.size)
            text.append((buffer.toString(Charsets.UTF_8).substring(0,size)))
        }
        catch (e:Exception)
        {
            check_connected = false
            Log.d("error_connect_Read",e.message.toString())
        }
        return check_connected;
    }
lateinit var s:Socket;
    override fun onStart() {
        super.onStart()

        array_data.add("Server online")
        list.adapter = ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,array_data)
        Thread({
            s = Socket("185.228.232.214",25565)
            //parametros
            val read:InputStream = s.getInputStream()
            val write:OutputStream = s.getOutputStream()
            var text_result = StringBuilder()
            var check_connect:Boolean = false
            var text_send = ""
            //set me
            text_send = "0"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("read", text_result.toString())

            text_result.clear()
            text_send = "get_users"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            var select = ""
            if(text_result.toString().length > 1) {
                val li = text_result.toString().split(',')
                 select = li[li.size-1]
            }
            else
            {
                select = text_result.toString()
            }

            text_send = "connect"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            text_send = select
            //
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = "1"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = "ssh"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = intent.getStringExtra("host").toString()
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = intent.getStringExtra("user").toString()
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = intent.getStringExtra("password").toString()
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = "df --si"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            //add
            array_data.add("_______memory___________")
            array_data.add(text_result.toString())
            //
            text_result.clear()
            //
            text_send = "ps"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            //add
            array_data.add("_______process___________")
            array_data.add(text_result.toString())
            //
            text_result.clear()
            //
            text_send = "close"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            text_result.clear()
            //
            text_send = "uname -a"
            Write(write,text_send)
            Read(read, text_result)
            Log.d("read", text_result.toString())
            //add
            array_data.add("_______Linux_OS___________")
            array_data.add(text_result.toString())
            //
            text_result.clear()
            //
            /*Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")
            //get access connect
            text_send = "get_users"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")
            // ___ ___ __ __
            val array:ArrayList<String> = text_result.split(',') as ArrayList<String>
            var select_user_connect = array[array.size-1]
            Log.d("select_user_connect",select_user_connect)
            //
            text_send = "connect"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")
            //setup me
            text_send = select_user_connect
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")

            //get information server
            text_send ="host"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")

            text_send ="user"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")

            text_send ="password"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")

            text_send ="1"
            text_result.clear()
            check_connect = Write(write, text_send)
            check_connect = Read(read, text_result)
            Log.d("static_information","SF: read: $text_result write: $text_send check_connect: $check_connect")
*/
            s.close()
                runOnUiThread({
                    list.adapter = ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,array_data)
                })

        }).start()

    }

    override fun onDestroy() {
        super.onDestroy()
       /* s.close()*/
    }

}