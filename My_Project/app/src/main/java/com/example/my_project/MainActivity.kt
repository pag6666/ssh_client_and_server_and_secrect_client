package com.example.my_project

import android.content.Intent
import android.os.Bundle
import android.text.BoringLayout
import android.view.View
import android.view.animation.Animation
import android.widget.EditText
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.airbnb.lottie.LottieAnimationView
import java.io.Serializable
import java.net.Socket
import java.util.Objects

class MainActivity : AppCompatActivity() {
    private fun checkEmptyStrin(text:String):Boolean
    {
        var check_strign_on_empty:Boolean = false;
        if(text.length > 0 && text != null)
        {
            check_strign_on_empty = true
        }
        else
        {
            check_strign_on_empty = false
        }
        return  check_strign_on_empty;
    }
    var _password:String = "";
    var _user:String = "";
    var _host:String = "";
    lateinit var password_edit:EditText
    lateinit var user_edit:EditText
    lateinit var host_index4_edit:EditText
    lateinit var host_index3_edit:EditText
    lateinit var host_index2_edit:EditText
    lateinit var host_index1_edit:EditText
    private fun Init()
    {
         password_edit = findViewById(R.id.password_edit)
         user_edit = findViewById(R.id.user_edit)
         host_index1_edit = findViewById(R.id.host_edit_1)
         host_index2_edit = findViewById(R.id.host_edit_2)
         host_index3_edit = findViewById(R.id.host_edit_3)
         host_index4_edit = findViewById(R.id.host_edit_4)
    }
    private fun set_string_parametros():Boolean
    {
        var check_string_edit:Boolean = true;
        if(checkEmptyStrin(user_edit.text.toString()))
        {
            _user = user_edit.text.toString()
        }
        else
        {
            //error_user
            Toast.makeText(this,"user empty",Toast.LENGTH_SHORT).show();
            check_string_edit = false
        }
        if(checkEmptyStrin(user_edit.text.toString()))
        {
            _password = password_edit.text.toString()
        }
        else
        {
            //error_password
            Toast.makeText(this,"password empty",Toast.LENGTH_SHORT).show();
            check_string_edit = false
        }
        if(checkEmptyStrin(host_index1_edit.text.toString()) && checkEmptyStrin(host_index2_edit.text.toString()) && checkEmptyStrin(host_index3_edit.text.toString()) && checkEmptyStrin(host_index4_edit.text.toString()))
        {
            _host ="${host_index1_edit.text.toString()}.${host_index2_edit.text.toString()}.${host_index3_edit.text.toString()}.${host_index4_edit.text.toString()}"
        }
        else
        {
            //error_host
            Toast.makeText(this,"host empty",Toast.LENGTH_SHORT).show();
            check_string_edit = false
        }
        return check_string_edit
    }

    //next activity
    private fun NextActivity()
    {
            var intent_activity: Intent = Intent(this, StartActivity::class.java)
            intent_activity.putExtra("host", _host)
            intent_activity.putExtra("user", _user)
            intent_activity.putExtra("password", _password)
            startActivity(intent_activity)
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val animationView: LottieAnimationView = findViewById(R.id.animation);
        animationView.playAnimation()
        Init()
    }
    //app_complite
    public fun button_connection(view:View)
    {
        if(set_string_parametros())
        {
            Toast.makeText(this,"Success",Toast.LENGTH_SHORT).show()
            NextActivity()
        }
    }
}

