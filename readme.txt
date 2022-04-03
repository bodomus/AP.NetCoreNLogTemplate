[Вчера 12:02] Ревак Станислав Сергеевич
Пишем консольную утилиту

[Вчера 12:02] Ревак Станислав Сергеевич
2. Вызываем процедуру получаем списаок таб номеров

[Вчера 12:02] Ревак Станислав Сергеевич
3. По списку в ад находим информацию

[Вчера 12:03] Ревак Станислав Сергеевич
4 сохраняем ее в БД

профиль по сотрудникам офиса ( нужно цикл по ним потому что для каждого предприятия свой)
EXEC AD.Profile_Employee_Type_GetList
@Employee_Type_Id = 1



begin
спиков пользователей без логинов возвращяет в том числе поле таб номер
EXEC dbo.Access_User_GetList
@Profile_Id = < то что вернула AD.Profile_Employee_Type_GetList >,
@LOGIN = '{nil}'



BEGIN
в цикле



проверить логин по табельноме
если есть візвать



EXEC dbo.Access_User_Edit
@Access_User_Id = < то что вернула Access_User_GetList >,
@Employee_id = < то что вернула Access_User_GetList >,
@User_Index = < то что вернула Access_User_GetList >,
@OU_Name = < то что вернула Access_User_GetList >,
@LOGIN = <из AD>,
@SID = <из AD>,
@EMail = < то что вернула Access_User_GetList >,
@Business_Role_Id = < то что вернула Access_User_GetList >, ,
@Is_Send_To_AD = 0



end
end


Вместо

EXEC AD.Profile_Employee_Type_GetList
@Employee_Type_Id = 1



теперь эта процедура, без параметров

AD.Profile_Not_Shop_GetList

