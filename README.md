# oop2-2023-class2

Данный код представляет собой простейшую реализацию формата JSON https://www.json.org/json-ru.html. В данной реализации значения могут быть только целочисленными. В этом коде очень много недостатков и вам нужно их устранить.

1. Как видите тут сначала создаётся объект, а потом ему присваивается значение. Устраните неконсистентность объекта сразу после создания и заодно сделайте хранимое значение константным, что бы его нельзя было изменять после инициализации.
2. Как видим Json очень сложно инициализировать и т.к. значение, которое он хранит нельзя изменять, то это нельзя сделать в самом экземпляре класса. Приходится раскрывать внутреннюю структуру класса для его инициализации, что может негативно сказаться на возможности дорабатывать код в будущем:
   - реализуйте класс JsonBuilder, который будет заниматься построением Json (используйте Fluent Builder).
   - JsonBuilder должен поддерживать методы Add(string key, Json value) и Add(string key, int value) для добавления элементов в объект
   - JsonBuilder должен поддерживать методы Add(Json value) и Add(int value) для добавления элементов в массив
   - JsonBuilder должен уметь приводиться к типу Json
3. Теперь, когда у нас есть JsonBuilder не составит труда реализовать метод FromString который будет из текстовой строки строить Json
4. Объекты класса Json умеют сообщать свой тип, но не умеют давать доступ к содержимому. Реализуйте у базового класса Json метод int GetInt(), которыq позволяет получить значение (только если это kValue в противном случае кидать исключение) и перегрузите оператор `[]` (он должен работать как с kArray, так и с kObject в зависимости от переданного типа)
5. Напишите тесты для всех реализованных методов
