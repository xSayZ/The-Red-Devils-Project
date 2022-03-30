INCLUDE globals.ink

{ weapon_name == "": -> main | -> already_chose}


=== main ===
What is your favorite weapon?
    + [Bren]
        -> chosen("Bren")
    + [Lee Enfield]
        -> chosen("Lee Enfield")
    + [Sten]
        -> chosen("Sten")
        
=== chosen(weapon) ====
~ weapon_name = weapon
You chose {weapon}!
-> END

=== already_chose ===
You already chose {weapon_name}!
-> END