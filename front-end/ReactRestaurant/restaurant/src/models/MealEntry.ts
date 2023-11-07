interface MealEntry {
  id: number;
  mealId: number;
  atDay: string;
  reservationsCount: number;
  customerCanCancel: boolean;
  preparedCount: number;
  meal: Meal;
}
